using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public int scene;
    public PlayerController playerController;
    public void QuestionTrue()
    {
        SceneManager.LoadScene(scene + 1);

    }
    public void QuestionFalse()
    {
      
            playerController.ResumeGame();
        
        
    }

}
