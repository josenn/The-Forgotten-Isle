using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typewriter_effect : MonoBehaviour
{

    public float slowTypewriterSpeed = 15f;
    private float currentTypewriterSpeed;
    public float fastTypewriterSpeed = 30f;

    private void Update() {
        if (Input.GetKey(KeyCode.Space)){
            currentTypewriterSpeed = fastTypewriterSpeed;
        }
        else {
            currentTypewriterSpeed = slowTypewriterSpeed;
        }
    }

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * currentTypewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textLabel.text = textToType.Substring(0, charIndex);

            yield return null;

        }
        textLabel.text = textToType;
    }
}
