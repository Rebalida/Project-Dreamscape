using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Souce")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("BackGround")]
    public AudioClip backGround;

    [Header("Player SFX")]
    public AudioClip swordAttack;
    public AudioClip bowAttack;
    public AudioClip staffAttack;
    public AudioClip hammerAttack;

    [Header("Other SFX")]
    public AudioClip openNewArea;
    public AudioClip smallEnemiesDestroy;
    public AudioClip bossDestroy;
    public AudioClip dialogueOpener;
    public AudioClip weaponObtain;
    public AudioClip pickupObtain;
    public AudioClip locketSFX;

    public static AudioManager instance;
    private float fadeDuration = 2.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);   
        }
    }

    private void Start()
    {
        BGMSource.clip = backGround;
        BGMSource.Play();
    }

    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip newBGMClip)
    {
        BGMSource.Stop();
        BGMSource.clip = newBGMClip;
        BGMSource.Play();
    }
    public void StopAudio() 
    {
        StartCoroutine(FadeOutBGM());
    }
    private IEnumerator FadeOutBGM()
    {
        float startVolume = BGMSource.volume;
        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            BGMSource.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeDuration);
            yield return null;
        }

        BGMSource.Stop();
        BGMSource.volume = startVolume;
    }
}
