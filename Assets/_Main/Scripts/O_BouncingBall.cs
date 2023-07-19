using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Cinemachine;

public enum BouncingBallState {OnTransition, Deselected, Selected, OnDragging, OnMoving, MoveEnd }
public class O_BouncingBall : MonoBehaviour
{
    private static BouncingBallState currentState = BouncingBallState.Deselected;
    public static BouncingBallState GetState { get { return currentState; } }

    private Rigidbody rb;
    [SerializeField] private int numOfBounces = 3;

    private Vector3 lastVelocity;
    private float curSpeed;
    private Vector3 direction;
    private int curBounces = 0;
    private M_InputUnityEvent mIUE;
    public LayerMask layer_Player;
    public LayerMask layer_Ground;
    public float forceEnhancement;
    public float velocityLocker;

    public CinemachineVirtualCamera cam_Player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mIUE = M_InputUnityEvent.Instance;
        //mIUE.ScreenClickCanceled += ShootBall;
        mIUE.BallDragging += DrawShootTrajectory;
        mIUE.BallDragEnded += ShootBall;
    }

    private void Update()
    {
        Debug.Log(currentState);
        if (currentState == BouncingBallState.OnMoving) VelocityController();

    }

    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (curBounces >= numOfBounces) return;

        //curSpeed = lastVelocity.magnitude;
        //direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        //rb.velocity = direction * Mathf.Max(curSpeed, 0);
        //curBounces++;
    }

    public void VelocityController()
    {
        bool isGrounded = false;
        if (Physics.Raycast(transform.position,Vector3.down, 0.6f, layer_Ground))
        {
            isGrounded = true;
        }
        if (rb.velocity.magnitude < velocityLocker && isGrounded)
        {
            rb.velocity = Vector3.zero;
            currentState = BouncingBallState.Selected;
        }
    }

    private void DrawShootTrajectory()
    {
        if (currentState == BouncingBallState.OnDragging) {
            Vector3 shootDirection = -mIUE.touchPos;
            currentState = BouncingBallState.OnDragging;
        } 
    }

    private void ShootBall()
    {
        if (currentState == BouncingBallState.OnDragging)
        {
            Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(mIUE.touchPos);
            Vector3 shootDirection = -worldTouchPos;
            Debug.Log(shootDirection);
            rb.AddForce(new Vector3(shootDirection.x, shootDirection.y, shootDirection.z) * forceEnhancement, ForceMode.Impulse);
            currentState = BouncingBallState.OnMoving;
        }
    }

    public void SelectCharacter()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mIUE.touchPos);
        if (currentState == BouncingBallState.Deselected)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_Player)) {
                currentState = BouncingBallState.Selected;
                DOTween.To(() => cam_Player.m_Lens.OrthographicSize, x => cam_Player.m_Lens.OrthographicSize = x, 10, 1);
            } 
    }

    public void DeselectCharacter()
    {
        currentState = BouncingBallState.OnTransition;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mIUE.touchPos);
        if (currentState == BouncingBallState.Selected)
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layer_Player)) 
                DOTween.To(() => cam_Player.m_Lens.OrthographicSize, x => cam_Player.m_Lens.OrthographicSize = x, 20, 1).
                    OnComplete(() => { currentState = BouncingBallState.Deselected; });
    }
}
