using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FaceLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTR;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnim;
    private SpriteRenderer mySpriteRenderer;
    private KnockBack knockBack;
    private float startMoveSpeed;
    private DialogueUi dialogueUi;

    public DialogueUi DialogueUi => dialogueUi;

    public IInteractable Interactable { get; set; }

    private bool facingLeft = false;
    private bool isDashing = false;

    AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();

        dialogueUi = GameObject.Find("Dialogue UI").GetComponent<DialogueUi>();

        startMoveSpeed = moveSpeed;

        ActiveInventory.Instance.EquipStartingWeapon();
    }


    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {

        PlayerInput();

        if (Input.GetKeyDown(KeyCode.E) && dialogueUi.IsOpen == false)
        {
            Debug.Log("Interacting");
            audioManager.PlaySFX(audioManager.dialogueOpener);
            Interactable?.Interact(this);
        }
    }
    private void FixedUpdate()
    {
        if (dialogueUi.IsOpen) return;
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider() 
    {
        return weaponCollider;
    }

    private void PlayerInput() 
    {

        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnim.SetFloat("moveX", movement.x);
        myAnim.SetFloat("moveY", movement.y);


    }

    private void Move()
    {
        if (knockBack.GettingKnockBack || PlayerHealth.Instance.IsDead)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash() 
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();
            isDashing = true;
            //audioManager.PlaySFX(audioManager.dash);
            moveSpeed *= dashSpeed;
            myTR.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() 
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startMoveSpeed;
        myTR.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
