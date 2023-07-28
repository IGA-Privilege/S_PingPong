using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_BouncingBall : MonoBehaviour
{
    protected Rigidbody rb;
    public LayerMask layer_Player;
    public LayerMask layer_Ground;
    public float forceEnhancement;
    public float velocityLocker;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        VelocityController();
    }

    public void VelocityController()
    {
        bool isGrounded = false;
        if (Physics.Raycast(transform.position,Vector3.down, 0.6f, layer_Ground)) isGrounded = true;
        if (rb.velocity.magnitude < velocityLocker && isGrounded) rb.velocity = Vector3.zero;
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
