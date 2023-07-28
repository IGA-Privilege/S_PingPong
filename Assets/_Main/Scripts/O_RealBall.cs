using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class O_RealBall : O_BouncingBall
{
    public GameObject selfPrefab;
    private Vector3 shootVelocity;
    public GameObject particle;
    public CinemachineVirtualCamera cam_Player;

    void Start()
    {
        GetComponent<O_BallInput>().DragAction += DrawShootTrajectory;
        GetComponent<O_BallInput>().ReleaseAction += ShootBall;
    }

    private void DrawShootTrajectory()
    {
        shootVelocity = GetComponent<O_BallInput>().dragDirection;
        shootVelocity = new Vector3(shootVelocity.x, 0.5f, shootVelocity.y);
        M_GhostProjection.Instance.SimulateTrajectory(selfPrefab, transform.position, shootVelocity * forceEnhancement);
    }

    private void ShootBall()
    {
        rb.AddForce(shootVelocity * forceEnhancement, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(go, 2f);
    }
}
