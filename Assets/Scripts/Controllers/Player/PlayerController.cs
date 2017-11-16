using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public struct MouseInput {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    [SerializeField] private float speed;
    [SerializeField] private MouseInput mouseControl;

    private MoveController _moveController;
    public MoveController MoveController {
        get {
            if (_moveController == null) {
                _moveController = GetComponent<MoveController>();
            }
            return _moveController;
        }
    }

    private InputController playerInput;
    private Vector2 mouseInput;

	void Awake () {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
	}
	
	void Update () {
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / mouseControl.Damping.x);
        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.Sensitivity.x);
	}
}
