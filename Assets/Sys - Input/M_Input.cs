using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class M_Input : MonoBehaviour
{
    private static M_Input instance;
    public static M_Input Instance { get { return instance; } }
    private PlayerControls playerControls;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerControls = new PlayerControls();
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerDelta()
    {
        Debug.Log(playerControls.Gameplay.Look.ReadValue<Vector2>().normalized);
        return playerControls.Gameplay.Look.ReadValue<Vector2>().normalized;
    }

    public bool GetIsMoving()
    {
        bool _isMoving = playerControls.Gameplay.Move.phase == InputActionPhase.Started || playerControls.Gameplay.Move.phase == InputActionPhase.Performed;
        return _isMoving;
    }

    public bool GetIsRotating()
    {
        bool _isRotating = playerControls.Gameplay.Move.phase == InputActionPhase.Started || playerControls.Gameplay.Move.phase == InputActionPhase.Performed;
        return _isRotating;
    }
}
