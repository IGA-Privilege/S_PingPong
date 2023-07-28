using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_GhostBall : O_BouncingBall
{
    public void Shoot(Vector3 velocity)
    {
        rb.AddForce(velocity, ForceMode.Impulse);
    }
}
