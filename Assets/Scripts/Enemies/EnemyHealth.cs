using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private bool isBoss = false;

    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    AudioManager audioManager;

    [SerializeField] private HpBar hpBar;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
        hpBar.SetMaxHealth(startingHealth);
        if (isBoss)
        {
            BossManager gameManager = GameObject.FindWithTag("Boss Manager").GetComponent<BossManager>();
            gameManager.RegisterBossEnemy(this);
        }
    }

    public void TakeDamage(int damage) 
    {
        ScreenShakeManager.Instance.ShakeScreen();
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        hpBar.UpdateHealthBar(currentHealth);
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine() 
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    private void DetectDeath() 
    {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            PickUpSpawner pickUpSpawner = GetComponent<PickUpSpawner>();
            if (pickUpSpawner != null)
            {
                pickUpSpawner.DropItems();
            }
            audioManager.PlaySFX(audioManager.smallEnemiesDestroy);

            if (isBoss)
            {
                BossManager gameManager = GameObject.FindWithTag("Boss Manager").GetComponent<BossManager>();
                gameManager.UnregisterBossEnemy(this);
            }
            Destroy(gameObject);
        }
    }

}
