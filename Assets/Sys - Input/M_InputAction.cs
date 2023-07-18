using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class M_InputAction : MonoBehaviour
{
    private static M_InputAction instance;
    public static M_InputAction Instance { get { return instance; } }
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchDeltaAction;
    private InputAction touchDragAction;

    public bool isRotating;
    public Vector3 touchDelta;
    public Vector3 touchStartPos;
    public Vector3 touchPerformedPos;
    public Vector3 touchCanceledePos;
    public Action RotatingEnd;
    public Action ScreenClickStart;
    public Action ScreenOnDrag;
    public Action ScreenClickCanceled;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


        playerInput = GetComponent<PlayerInput>();
        //touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchDeltaAction = playerInput.actions["TouchDelta"];
        //touchDragAction = playerInput.actions["TouchDrag"];
    }

    private void OnEnable()
    {
        touchPressAction.started += TouchStart;
        //touchPressAction.performed += TouchPerformed;
        touchPressAction.canceled += TouchCanceled;
        touchDragAction.performed += TouchPerformed;
    }

    private void OnDisable()
    {
        touchPressAction.started -= TouchStart;
        //touchPressAction.performed -= TouchPerformed;
        touchPressAction.canceled -= TouchCanceled;
        touchDragAction.performed -= TouchPerformed;
    }

    private void TouchStart(InputAction.CallbackContext context)
    {
        touchStartPos = touchPositionAction.ReadValue<Vector2>();
        ScreenClickStart();
        isRotating = true;
    }

    private void TouchPerformed(InputAction.CallbackContext context)
    {
        touchPerformedPos = touchPositionAction.ReadValue<Vector2>();
        touchDelta = touchDeltaAction.ReadValue<Vector2>();
        ScreenOnDrag();
    }

    private void TouchCanceled(InputAction.CallbackContext context)
    {
        ScreenClickCanceled();
        touchCanceledePos = touchPositionAction.ReadValue<Vector2>();
        RotatingEnd();
        isRotating = false;
        touchDelta = Vector2.zero;
    }
}
