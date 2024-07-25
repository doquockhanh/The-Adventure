using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int startScene = 1;
    public void StartGame() {
        SceneManager.LoadScene(startScene);
    }

    public void Exit() {
        Application.Quit();
    }
}
