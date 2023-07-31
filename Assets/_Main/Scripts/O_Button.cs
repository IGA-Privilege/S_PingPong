using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class O_Button : MonoBehaviour
{
    public MMF_Player mmf_ButtonPress;
    public MMF_Player mmf_DoorOpen;
    private bool isClose = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && isClose)
        {
            isClose = false;
            mmf_ButtonPress.PlayFeedbacks();
        }
    }

    public void DoorOpen()
    {
        M_Game.Instance.PauseGame();
        mmf_DoorOpen.PlayFeedbacks();
    }
}
