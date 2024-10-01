using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupTrigger : MonoBehaviour
{
    [SerializeField] private WeaponInfo itemToPickup;
    [SerializeField] private int inventoryChildIndex;
    [SerializeField] private int equippedItemIndex;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (inventoryChildIndex >= 0 && inventoryChildIndex < ActiveInventory.Instance.transform.childCount)
            {
                InventorySlot inventorySlot = ActiveInventory.Instance.transform.GetChild(inventoryChildIndex).GetComponentInChildren<InventorySlot>();
                inventorySlot.SetWeaponInfo(itemToPickup);
            }

            if (UIManager.instance != null && equippedItemIndex < UIManager.instance.equippedItems.Count)
            {
                UIManager.instance.equippedItems[equippedItemIndex].SetActive(true);
            }
            audioManager.PlaySFX(audioManager.weaponObtain);
            Destroy(gameObject);
        }
    }
}
