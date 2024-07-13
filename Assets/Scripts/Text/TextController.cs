using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public bool textFadeOut = true;
    public float duration = 2.5f;
    public float sizeScale = 1f;
    public string text = "default text";

    void Start()
    {
        Text textObject = GetComponent<Text>();
        textObject.text = text;
        textObject.transform.localScale = new Vector3(sizeScale, sizeScale, 0);
        if (textFadeOut)
        {
            StartCoroutine(FadeOutAndDestroy(duration));
        }
    }

    IEnumerator FadeOutAndDestroy(float duration)
    {
        Text damageText = gameObject.GetComponent<Text>();

        Vector3 startPos = gameObject.transform.position;

        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            gameObject.transform.position = startPos + Vector3.up * t;
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1f - t);
            yield return null;
        }

        Destroy(gameObject);
    }
}
