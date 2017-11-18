using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(MoveController))]
public class PlayerController : NetworkBehaviour {

    [System.Serializable]
    public struct MouseInput {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private MouseInput mouseControl;
    [SerializeField] private Animator animator;

    private Rigidbody rig;
    private bool jumping = false;

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

	void Start () {
        if (!isLocalPlayer)
        {
            enabled = false;
            return;
        }
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        rig = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 1.5f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 1.5f;

        bool grounded = Grounded();

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumping = true;

            rig.AddForce(transform.up * jumpForce);
        }

        animator.SetFloat("Speed_f", Mathf.Abs(playerInput.Vertical) * speed);
        
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / mouseControl.Damping.x);
        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.Sensitivity.x);
	}

    bool Grounded()
    {
        bool ret = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (ret && jumping) {
            jumping = false;
            animator.SetBool("Jump_b", jumping);
        }
        return ret;
    }
}
