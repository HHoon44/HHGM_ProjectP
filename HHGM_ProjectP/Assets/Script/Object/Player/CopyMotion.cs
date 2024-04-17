using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform targetLimb;
    public bool mirror;

    private ConfigurableJoint cj;

    private void Awake()
    {
        cj = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if (!mirror)
        {
            cj.targetRotation = targetLimb.rotation;
        }
        else
        {
            cj.targetRotation = targetLimb.rotation;
        }
    }
}