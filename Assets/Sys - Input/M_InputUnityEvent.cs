using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Cinemachine;
using DG.Tweening;

public class M_InputUnityEvent : MonoBehaviour
{
    public bool isRotating;
    public Vector3 touchDelta;
    public Vector3 touchPos;
    public Vector3 dragStartPosition;

    public Action ScreenPressStarted;
    public Action ScreenPressCanceled;
    public Action BallDragging;
    public Action BallDragEnded;

    public CinemachineVirtualCamera cam_Player;

    private void Start()
    {
        
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        BallDragging();
        if (context.started)
        {
            DOTween.To(() => cam_Player.m_Lens.OrthographicSize, x => cam_Player.m_Lens.OrthographicSize = x, 10, 1);
            dragStartPosition = context.ReadValue<Vector2>();
        }
        touchPos = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            BallDragEnded();
            DOTween.To(() => cam_Player.m_Lens.OrthographicSize, x => cam_Player.m_Lens.OrthographicSize = x, 20, 1);
        }
    }

    //public void OnTouchDelta(InputAction.CallbackContext context)
    //{
    //    if (O_BouncingBall.GetState == BouncingBallState.Deselected)
    //        isRotating = context.started || context.performed;

    //    touchDelta = context.ReadValue<Vector2>();
    //}
}
