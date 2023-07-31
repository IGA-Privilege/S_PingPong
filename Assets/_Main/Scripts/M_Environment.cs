using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Environment : Singleton<M_Environment>
{
    public Material mat_Normal;
    private bool isTrans = false;
    private bool isPreviousPressed = false;

    void Start()
    {
        ChangeMaterial_ToNormal();
    }

    // Update is called once per frame
    void Update()
    {
        PressStateChanged();
    }

    public void ChangeMaterial_ToNormal()
    {
        Debug.Log("To Normal");
        mat_Normal.SetColor("_BaseColor", new Color(1, 1, 1, 1));

    }

    public void ChangeMaterial_ToTrans()
    {
        Debug.Log("To Trans");
        mat_Normal.SetColor("_BaseColor", new Color(1, 1, 1, 0.1f));
    }

    public void PressStateChanged()
    {
        if (O_BallInput.Instance.isPressed != isPreviousPressed)
        {
            if (O_BallInput.Instance.isPressed)
            {
                ChangeMaterial_ToTrans();
                isPreviousPressed = true;
            }
            else
            {
                //ChangeMaterial_ToNormal();
                isPreviousPressed = false;
            }
        }
    }
}
