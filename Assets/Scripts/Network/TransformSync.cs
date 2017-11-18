﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TransformSync : NetworkBehaviour {
    [SerializeField] private bool position;
    [SerializeField] private bool rotation;

    [SyncVar] private Vector3 syncPos;
    [SyncVar] private Quaternion syncRot;

    [SerializeField] private Transform myTransform;
    [SerializeField] private float lerpRate = 15;

    private void FixedUpdate()
    {
        if (position)
        {
            TransmitPosition();
            LerpPosition();
        }
        if (rotation)
        {
            TransmitRotation();
            LerpRotation();
        }
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdProvidePositionToServer(myTransform.position);
        }
    }

    void LerpRotation()
    {
        if (!isLocalPlayer)
        {
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRot, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationToServer(Quaternion rot)
    {
        syncRot = rot;
    }

    [ClientCallback]
    void TransmitRotation()
    {
        if (isLocalPlayer)
        {
            CmdProvideRotationToServer(myTransform.rotation);
        }
    }
}
