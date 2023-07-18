using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class M_InputUnityEvent : MonoBehaviour
{
    private static M_InputUnityEvent instance;
    public static M_InputUnityEvent Instance { get { return instance; } }

    public bool isRotating;
    public Vector3 touchDelta;
    public Vector3 touchPos;
    public Vector3 previousPos;

    public Action ScreenPressStarted;
    public Action ScreenPressCanceled;
    public Action BallDragging;
    public Action BallDragEnded;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else { instance = this; DontDestroyOnLoad(gameObject); }
    }

    private void Start()
    {
        
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        BallDragging();
        touchPos = context.ReadValue<Vector2>();
    }

    public void OnTouchPress(InputAction.CallbackContext context)
    {
        ScreenPressStarted();
        //if (context.started) ScreenPressStarted();
        if (context.canceled) {
            if (O_BouncingBall.GetState == BouncingBallState.OnDragging) BallDragEnded();
            else ScreenPressCanceled();

            touchPos = Vector3.zero;
        }

    }

    public void OnTouchDelta(InputAction.CallbackContext context)
    {
        if (O_BouncingBall.GetState == BouncingBallState.Deselected)
            isRotating = context.started || context.performed;

        touchDelta = context.ReadValue<Vector2>();
    }
}
