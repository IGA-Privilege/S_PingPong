using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;

public class M_Game : Singleton<M_Game>
{
    public MMF_Player mmf_End;
    public MMF_Player mmf_Start;
    private bool isGamePaused = false;
    public bool GetGamePauseState { get { return isGamePaused; } }

    private void Start()
    {
        mmf_Start.PlayFeedbacks();
    }

    public void ReloadGame()
    {
        ContinueGame();
        if (FindObjectOfType<O_RealBall>().IsWin) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }
}
