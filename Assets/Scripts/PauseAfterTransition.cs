using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseAfterTransition : MonoBehaviour
{
    public void StartPause()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
           // ManageGameState.gameIsPaused = true;
          //  Time.timeScale = 0f;
        }
    }
}
