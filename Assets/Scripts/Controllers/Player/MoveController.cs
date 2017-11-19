using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    private Camera TPSCamera;

    [System.Serializable]
    public class OtherSettings
    {
        public float lookSpeed = 5.0f;
        public float lookDistance = 30.0f;
        public bool requireInputForTurn = true;
        public LayerMask aimDetectionLayers;
    }
    [SerializeField]
    public OtherSettings other;

    void Start()
    {
        TPSCamera = Camera.main;
    }

    public void Move(Vector2 direction) {
        CharacterLook();
        transform.position += transform.forward * direction.x * Time.deltaTime + transform.right * direction.y * Time.deltaTime;
    }

    void CharacterLook()
    {
        Transform mainCamT = TPSCamera.transform;
        Transform pivotT = mainCamT.parent;
        Vector3 pivotPos = pivotT.position;
        Vector3 lookTarget = pivotPos + (pivotT.forward * other.lookDistance);
        Vector3 thisPos = transform.position;
        Vector3 lookDir = lookTarget - thisPos;
        Quaternion lookRot = Quaternion.LookRotation(lookDir);
        lookRot.x = 0;
        lookRot.z = 0;

        Quaternion newRotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * other.lookSpeed);
        transform.rotation = newRotation;
    }
}
