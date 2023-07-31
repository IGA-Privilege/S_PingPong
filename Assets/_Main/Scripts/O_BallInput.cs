using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class O_BallInput : Singleton<O_BallInput>
{
    [SerializeField] private InputAction press, touchPos;
    private Vector3 curScreenPos;
    private Camera curCamera;
    private bool isDragging = false;

    public Action PressAction;
    public Action DragAction;
    public Action ReleaseAction;

    private Vector3 pos_Start;
    private Vector3 pos_End;

    public bool isPressed = false;
    public Transform realBall;

    //private bool isClickedOn
    //{
    //    get
    //    {
    //        Ray ray = curCamera.ScreenPointToRay(curScreenPos);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            return hit.transform == transform;
    //        }
    //        return false;
    //    }
    //}

    public Vector3 worldPos
    {
        get
        {
            float z = curCamera.WorldToScreenPoint(transform.position).z;
            return curCamera.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
        }
    }

    public Vector3 dragDirection { get { return (pos_Start - curScreenPos).normalized; } }

    private void Awake()
    {
        curCamera = Camera.main;
        press.Enable();
        touchPos.Enable();
    }

    private void Start()
    {
        realBall = FindObjectOfType<O_RealBall>().transform;
        touchPos.started += context => { pos_Start = context.ReadValue<Vector2>(); };
        touchPos.performed += context => { curScreenPos = context.ReadValue<Vector2>(); };
        press.started += _ => isPressed = true;
        press.performed += _ => { StartCoroutine(Drag()); };
        press.canceled += _ => { isDragging = false; };
        press.canceled += _ => isPressed = false;
        touchPos.canceled += context => { pos_End = context.ReadValue<Vector2>(); };
    }

    private IEnumerator Drag()
    {
        isDragging = true;
        pos_Start = curScreenPos;
        if(PressAction!=null) PressAction();
        while (isDragging)
        {
            DragAction();
            yield return null;
        }
        pos_End = curScreenPos;
        ReleaseAction();
    }
}
