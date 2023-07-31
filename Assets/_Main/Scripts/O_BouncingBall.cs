using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class O_BouncingBall : MonoBehaviour
{
    protected Rigidbody rb;
    public LayerMask layer_Player;
    public LayerMask layer_Ground;
    public float forceEnhancement;
    public float velocityLocker;
    protected bool isOnFly = false;
    public Action FlyEndAction;
    protected bool isGrounded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        FlyEndAction += () => isOnFly = false;
        //FlyEndAction += () => rb.velocity = Vector3.zero;
    }

    void Update()
    {
        VelocityController();
    }

    public void VelocityController()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.6f, layer_Ground)) isGrounded = true;
        else isGrounded = false;
        if (rb.velocity.magnitude < velocityLocker && isGrounded) 
        {
            if (isOnFly) FlyEndAction();
            rb.velocity = Vector3.zero;
        }
   
        //{
        //    isOnFly = false;
        //    rb.velocity = Vector3.zero;
        //}
    }

    //public void SelectCharacter()
    //{
    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(mIUE.touchPos);
    //    if (currentState == BouncingBallState.Deselected)
    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_Player)) {
    //            currentState = BouncingBallState.Selected;
    //            DOTween.To(() => cam_Player.m_Lens.OrthographicSize, x => cam_Player.m_Lens.OrthographicSize = x, 10, 1);
    //        } 
    //}

    //public void DeselectCharacter()
    //{
    //    currentState = BouncingBallState.OnTransition;
    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(mIUE.touchPos);
    //    if (currentState == BouncingBallState.Selected)
    //        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layer_Player)) 
    //            DOTween.To(() => cam_Player.m_Lens.OrthographicSize, x => cam_Player.m_Lens.OrthographicSize = x, 20, 1).
    //                OnComplete(() => { currentState = BouncingBallState.Deselected; });
    //}
}
