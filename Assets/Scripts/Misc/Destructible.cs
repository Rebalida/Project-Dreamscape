using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DamageClass>() || other.gameObject.GetComponent<Projectile>())
        {
            PickUpSpawner pickUpSpawner = GetComponent<PickUpSpawner>();
            pickUpSpawner?.DropItems();
            GetComponent<PickUpSpawner>().DropItems();
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            DestructibleSound destructibleSound = GetComponent<DestructibleSound>();
            if (destructibleSound != null)
            {
                destructibleSound.PlayDestroySound();
            }
            Destroy(gameObject);
        }
    }
}
