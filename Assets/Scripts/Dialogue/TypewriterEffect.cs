using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f;

    public static TypewriterEffect instance;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'!'}, 0.3f),
        new Punctuation(new HashSet<char>() {';', ':'}, 0.3f)
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType, TMP_Text textLabel) 
    {
        typingCoroutine = StartCoroutine(TypeTextRoutine(textToType, textLabel));
    }

    public void Stop() 
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeTextRoutine(string textToType, TMP_Text textLabel) 
    {
        IsRunning = true;

        textLabel.text = string.Empty;

        float t = 0f;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = 1 >= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i + 1);

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }
        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime) 
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character)) 
            {
                waitTime = punctuationCategory.WaitTIme;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation 
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTIme;

        public Punctuation(HashSet<char> punctuations, float waitTime) 
        {
            Punctuations = punctuations;
            WaitTIme = waitTime;
        }
    }
}
