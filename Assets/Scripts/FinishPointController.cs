using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPointController : MonoBehaviour
{
    public int indexNextLevel = 0;
    public GameObject winPanel;

    void Start()
    {
        winPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        winPanel.SetActive(false);
        Time.timeScale = 1f;
    }


    public void NextLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(indexNextLevel);
    }

    public void ReloadLevel()
    {
        ResumeGame();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
}
