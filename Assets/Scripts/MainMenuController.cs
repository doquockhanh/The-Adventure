using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int startScene = 4;
    public void StartGame() {
        SceneManager.LoadScene(4);
    }

    public void Exit() {
        Application.Quit();
    }
}
