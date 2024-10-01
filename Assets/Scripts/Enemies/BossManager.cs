using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public List<GameObject> obstacles;
    AudioManager audioManager;

    private List<EnemyHealth> bossEnemies = new List<EnemyHealth>();
    [SerializeField] private bool stopAudioOnBossDeath = true;

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
            if (stopAudioOnBossDeath)
            {
                audioManager.StopAudio();
            }


            foreach (GameObject obstacle in obstacles)
            {
                Destroy(obstacle);
            }

            audioManager.PlaySFX(audioManager.openNewArea);
        }
    }
    public void SetStopAudioOnBossDeath(bool shouldStop)
    {
        stopAudioOnBossDeath = shouldStop;
    }
}
