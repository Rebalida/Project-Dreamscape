using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocketUI : MonoBehaviour
{
    public Image locketImage;

    public GameObject locket;

    private void Start()
    {
        locketImage.enabled = false;
    }
    public void ShowLocket()
    {
        locketImage.enabled = true;
    }

    public void HideLocket()
    {
        locketImage.enabled = false;
    }

    public void OnLocketCollected()
    {
        if (locket != null)
        {
            Destroy(locket);
            ShowLocket();
        }
    }
}
