using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelfDestroy : MonoBehaviour
{
    public float destroyTime = 1.0f;
    public float fadeDuration = 0.5f;

    private TextMeshProUGUI textMeshPro;
    private CanvasGroup canvasGroup;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        Invoke("FadeOutAndDestroy", destroyTime);
    }

    void FadeOutAndDestroy()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;

        Destroy(gameObject);
    }
}

