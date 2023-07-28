using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Environment : MonoBehaviour
{
    public Material mat_Normal;
    public Material mat_Transparent;
    private bool isTrans = false;
    public Transform wallParent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMaterial_ToNormal()
    {
        if (isTrans)
        {
            for (int i = 0; i < wallParent.childCount; i++)
                wallParent.GetChild(i).GetComponent<MeshRenderer>().material = mat_Transparent;
            isTrans = true;
        }
        else
        {
            for (int i = 0; i < wallParent.childCount; i++)
                wallParent.GetChild(i).GetComponent<MeshRenderer>().material = mat_Normal;
            isTrans = false;
        }
    }

    public void ChangeMaterial_ToTrans()
    {
        if (isTrans)
        {
            for (int i = 0; i < wallParent.childCount; i++)
                wallParent.GetChild(i).GetComponent<MeshRenderer>().material = mat_Transparent;
            isTrans = true;
        }
        else
        {
            for (int i = 0; i < wallParent.childCount; i++)
                wallParent.GetChild(i).GetComponent<MeshRenderer>().material = mat_Normal;
            isTrans = false;
        }
    }
}
