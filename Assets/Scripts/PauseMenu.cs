using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public string title;
    public bool haveResumeOption = true;
    public bool canEsc = true;
    private string defaultTitle = "Game Paused!";

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canEsc)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        if(title != null) {
            pauseMenuUI.GetComponentInChildren<Text>().text = title;
            title = null;
        }else {
            pauseMenuUI.GetComponentInChildren<Text>().text = defaultTitle;
        }

        if(!haveResumeOption) {
            pauseMenuUI.GetComponentsInChildren<Button>()[0].gameObject.SetActive(false);
        }
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        haveResumeOption = true;
        canEsc = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReplayLevel()
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
