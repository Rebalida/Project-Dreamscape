using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject obstacle;
    AudioManager audioManager;

    private List<EnemyHealth> bossEnemies = new List<EnemyHealth>();

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void RegisterBossEnemy(EnemyHealth boss)
    {
        bossEnemies.Add(boss);
    }

    public void UnregisterBossEnemy(EnemyHealth boss)
    {
        bossEnemies.Remove(boss);
        if (bossEnemies.Count == 0)
        {

            Destroy(obstacle);
            audioManager.PlaySFX(audioManager.openNewArea);
        }
    }
}
