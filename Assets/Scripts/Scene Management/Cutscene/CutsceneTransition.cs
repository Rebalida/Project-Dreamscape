using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTransition : MonoBehaviour
{
    public string nextSceneName;
    [SerializeField] private AudioClip newSceneBGM;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = PlayerController.Instance;
            if (newSceneBGM != null)
            {
                AudioManager.instance.ChangeBGM(newSceneBGM);
            }
            if (playerController != null)
            {
                playerController.DestroyPlayer();
            }
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
