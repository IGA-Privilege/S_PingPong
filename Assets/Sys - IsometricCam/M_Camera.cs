using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Cinemachine;

public class M_Camera : MonoBehaviour
{
    //public M_InputUnityEvent mIUE;
    //[SerializeField] private float moveSpeed = 10;
    //[SerializeField] private float rotateSpeed = 0.5f;
    //private float xRotation;
    //private bool isCamRotationSnap;
    //private CinemachineVirtualCamera cam_Player;
    public Transform target;
    //private O_BouncingBall o_BBall;
    private Vector3 targetPosOffset;

    void Start()
    {
        //cam_Player = GetComponent<CinemachineVirtualCamera>();
        //o_BBall = target.GetComponent<O_BouncingBall>();
        //xRotation = transform.rotation.eulerAngles.x;
        CameraTargetPosOffsetAchieve();
    }

    private void LateUpdate()
    {
        //CameraRotating();
        CameraFollowing();
    }

    //void CameraMoving()
    //{
    //    if (isCamRotationSnap) return;
    //    if (mIUE.isMoving)
    //    {
    //        Vector3 position = transform.right * (mIUE.delta.x * -moveSpeed);
    //        position += transform.up * (mIUE.delta.y * -moveSpeed);
    //        transform.position += position * Time.deltaTime;
    //    }
    //}

    void CameraFollowing()
    {
        transform.position = target.position + targetPosOffset;
    }

    void CameraTargetPosOffsetAchieve()
    {
        targetPosOffset = transform.position - target.position;
    }

    //public void SnapRotation()
    //{
    //    isCamRotationSnap = true;
    //    transform.DORotate(SnappedVector(), 0.5f).SetEase(Ease.OutBounce).OnComplete(
    //        () => { isCamRotationSnap = false; CameraTargetPosOffsetAchieve(); });

    //    Vector3 SnappedVector()
    //    {
    //        float endValue = 0.0f;
    //        float currentY = Mathf.Ceil(transform.rotation.eulerAngles.y);

    //        endValue = currentY switch
    //        {
    //            >= 0 and <= 90 => 45f,
    //            >= 91 and <= 180 => 135f,
    //            >= 181 and <= 270 => 225f,
    //            _ => 315f,
    //        };
    //        return new Vector3(xRotation, endValue, 0);
    //    }
    //}
}

//    void CameraRotating()
//    {
//        if (mIUE.isRotating && O_BouncingBall.GetState == BouncingBallState.Deselected)
//        {
//            transform.Rotate(new Vector3(xRotation, mIUE.touchDelta.x * rotateSpeed, 0.0f));
//            transform.RotateAround(target.position, Vector3.up, transform.position.y);
//            transform.rotation = Quaternion.Euler(xRotation, transform.rotation.eulerAngles.y, 0.0f);
//        }
//    }


//}
