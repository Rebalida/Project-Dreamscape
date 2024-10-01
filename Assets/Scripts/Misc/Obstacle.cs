using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private bool isObstacleOpen = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OpenObstacle()
    {
        if (!isObstacleOpen)
        {
            gameObject.SetActive(false);
            audioManager.PlaySFX(audioManager.openNewArea);
            isObstacleOpen = true;
        }
    }

    public void CloseObstacle()
    {
        if (isObstacleOpen)
        {
            gameObject.SetActive(true);
            isObstacleOpen = false;
        }
    }
}