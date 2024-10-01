using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthGlobe, staminaGlobe, locket;
    private bool locketDropped = false;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        if (randomNum == 1 && healthGlobe != null)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 2 && staminaGlobe != null)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
        if (!locketDropped && locket != null)
        {
            Instantiate(locket, transform.position, Quaternion.identity);
            locketDropped = true; 
        }
    }
}
