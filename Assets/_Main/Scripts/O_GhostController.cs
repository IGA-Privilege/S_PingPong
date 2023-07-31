using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class O_GhostController : MonoBehaviour
{
    public bool isRotatable;
    public float rotateTime;
    public Color color_Default;
    public Color color_Detected;

    private float rotateTimer;
    private bool detected = false;

    [SerializeField] ConeviewDetector detector;
    [SerializeField] MeshRenderer detectionRenderer;
    [SerializeField] private MMF_Player mmf_Rotate;
    [SerializeField] private MMF_Player mmf_Alart;

    private void Start()
    {
        ResetRotateTimer();
        detectionRenderer.material.SetColor("_BaseColor", color_Default);
    }

    private void Update()
    {
        if (isRotatable) RotateAction();
        if (!detected) DetectedAction();
    }

    void RotateAction()
    {
        rotateTimer -= Time.deltaTime;
        if (rotateTimer < 0)
        {
            ResetRotateTimer();
            mmf_Rotate.PlayFeedbacks();
        }
    }

    void ResetRotateTimer()
    {
        rotateTimer = rotateTime;
    }

    private void DetectedAction()
    {
        Debug.Log(detector.visibleTargets.Count != 0 ? "Yes" : "No");
        if (detector.visibleTargets.Count!=0 && !detected)
        {
            M_Game.Instance.PauseGame();
            detected = true;
            detectionRenderer.material.SetColor("_BaseColor", color_Detected);
            mmf_Rotate.StopFeedbacks();
            mmf_Alart.PlayFeedbacks();
        }
    }

    public void CallRestartGame()
    {
        M_Game.Instance.mmf_End.PlayFeedbacks();
    }
}
