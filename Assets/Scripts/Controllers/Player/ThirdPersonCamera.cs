using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float damping;

    private Transform cameraLookTarget;
    private PlayerController localPlayer;

    void Awake () {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}

    void HandleOnLocalPlayerJoined(PlayerController player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("CameraLookTarget");

        if (cameraLookTarget == null) {
            cameraLookTarget = localPlayer.transform;
        }
    }

    // Update is called once per frame
    void Update () {
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z +
                                                 localPlayer.transform.up * cameraOffset.y +
                                                 localPlayer.transform.right * cameraOffset.x;
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
	}
}
