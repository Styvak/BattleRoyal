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
    public bool Fire1;
    public bool Fire2;
    public bool Reload;
    public bool Num1;
    public bool Num2;
    public bool Num3;
    public bool Num4;
    public bool Num5;

	void Update () {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetAxis("Jump");
        //Crouch = Input.GetAxis("Crouch");
        //Sprint = Input.GetAxis("Sprint");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        Reload = Input.GetKeyDown(KeyCode.R);
        Num1 = Input.GetKeyDown(KeyCode.Ampersand);
        Num2 = Input.GetKeyDown(KeyCode.Alpha1);
        Num3 = Input.GetKeyDown(KeyCode.Alpha3);
        Num4 = Input.GetKeyDown(KeyCode.Alpha4);
        Num5 = Input.GetKeyDown(KeyCode.Alpha5);

        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }
        if (Num2) {
            Debug.Log("YEAAHHHHH");
        }
	}
}
