using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopUp : MonoBehaviour
{
    public Text popupText;

    [SerializeField]
    private string[] keyTexts = new string[5];

    private void Start()
    {
        popupText.CrossFadeAlpha(0f, 0f, false);

        for (int i = 0; i < keyTexts.Length; i++)
        {
            if (string.IsNullOrEmpty(keyTexts[i]))
            {
                keyTexts[i] = "Default Text for Key " + (i + 1);
            }
        }
    }

   private void Update()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                popupText.text = keyTexts[i - 1];

                popupText.CrossFadeAlpha(1f, 1f, false);

                Invoke("FadeOutText", 2f);
            }
        }
    }
    private void FadeOutText()
    {
        popupText.CrossFadeAlpha(0f, 1f, false);
    }
}
