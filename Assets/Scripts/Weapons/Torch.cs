using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;

    private Animator myAnim;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffSet();
    }

    public void Attack()
    {
        myAnim.SetTrigger(FIRE_HASH);

    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    private void MouseFollowWithOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
