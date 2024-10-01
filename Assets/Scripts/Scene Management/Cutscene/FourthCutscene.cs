using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourthCutscene : MonoBehaviour
{
    public float delayBeforeLoading = 2.0f;
    public AudioClip dockBackgroundMusic;


    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }


    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        SceneManager.LoadScene("End", LoadSceneMode.Single);
        AudioManager.instance.ChangeBGM(dockBackgroundMusic);
    }
}
