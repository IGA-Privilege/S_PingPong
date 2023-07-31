using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class M_OpenPage : MonoBehaviour
{
    public MMF_Player mmf_Land;

    private void OnCollisionEnter(Collision collision)
    {
        mmf_Land.PlayFeedbacks();
    }

    public void OnClickStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
