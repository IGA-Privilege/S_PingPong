using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MoreMountains.Feedbacks;

public class O_RealBall : O_BouncingBall
{
    public GameObject selfPrefab;
    private Vector3 shootVelocity;
    public GameObject particle;
    public CinemachineVirtualCamera cam_Player;
    public GameObject particleWin;

    public MMF_Player mmf_Shoot;
    public MMF_Player mmf_Hit;
    public MMF_Player mmf_Land;
    private bool isPlayerWin = false;
    public bool IsWin { get { return isPlayerWin; } }

    private float timer;
    public float lifeTime;

    void Start()
    {
        O_BallInput.Instance.DragAction += DrawShootTrajectory;
        O_BallInput.Instance.ReleaseAction += ShootBall;
        FlyEndAction += M_Environment.Instance.ChangeMaterial_ToNormal;
        ResetTimer();
    }

    private void LateUpdate()
    {
        if (isGrounded) ResetTimer();
        else LifeTimeDetection();
    }

    private void DrawShootTrajectory()
    {
        shootVelocity = O_BallInput.Instance.dragDirection;
        shootVelocity = new Vector3(shootVelocity.x, 0.5f, shootVelocity.y);
        M_GhostProjection.Instance.SimulateTrajectory(selfPrefab, transform.position, shootVelocity * forceEnhancement);
    }

    private void ShootBall()
    {
        isOnFly = true;
        mmf_Shoot.PlayFeedbacks();
        rb.AddForce(shootVelocity * forceEnhancement, ForceMode.Impulse);
        M_GhostProjection.Instance.EraseTrajectory();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && isOnFly)
        {
            mmf_Land.PlayFeedbacks();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            mmf_Hit.PlayFeedbacks();
        }

        if (rb.velocity.magnitude > velocityLocker)
        {
            GameObject go = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(go, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndArea") && !isPlayerWin)
        {
            isPlayerWin = true;
            Instantiate(particleWin, transform.position, Quaternion.identity);
            M_Game.Instance.mmf_End.PlayFeedbacks();
        }
    }

    private void LifeTimeDetection()
    {
        timer -= Time.deltaTime;
        //Debug.Log(timer);
        if (timer <= 0) M_Game.Instance.mmf_End.PlayFeedbacks();
    }

    private void ResetTimer()
    {
        timer = lifeTime;
    }
}
