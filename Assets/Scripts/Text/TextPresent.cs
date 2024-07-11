using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public float delayBetweenCharacters = 0.1f; 
    public GameObject[] buttonsToActivate; 
    private TMP_Text tmpText;
    private string fullText;
    private string currentText = "";
    private int currentIndex = 0;
    private float timer = 0f;
    private bool textComplete = false;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>(); 
        fullText = tmpText.text;
        tmpText.text = ""; 

       
        foreach (GameObject button in buttonsToActivate)
        {
            button.SetActive(false);
        }
    }

    void Update()
    {
        if (!textComplete)
        {
            timer += Time.deltaTime;
            if (timer >= delayBetweenCharacters && currentIndex < fullText.Length)
            {
                currentText += fullText[currentIndex];
                tmpText.text = currentText;
                currentIndex++;
                timer = 0f;
            }
            else if (currentIndex >= fullText.Length)
            {
                textComplete = true;
                ShowButtons();
            }
        }
    }

    void ShowButtons()
    {
       
        foreach (GameObject button in buttonsToActivate)
        {
            button.SetActive(true);
        }
    }
}