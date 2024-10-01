using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSound : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayDestroySound()
    {
        audioManager.PlaySFX(destroySound);
    }
}