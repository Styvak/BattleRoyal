using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float Vertical;
    public float Horizontal;
    public float Jump;
    public float Crouch;
    public float Sprint;
    public Vector2 MouseInput;

	void Update () {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetAxis("Jump");
        Crouch = Input.GetAxis("Crouch");
        Sprint = Input.GetAxis("Sprint");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
	}
}
