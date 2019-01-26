using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    CharacterController cc;
    //move speed modifier
    float speed = 10f;
    //gravity
    float gravity = -15f;
    float ySpeed = 0;
    //camera control
    public Transform fpsCamera;
    float sensitivity = 10f;
    float pitch = 0f;
    float lookMin = -55f;
    float lookMax = 55f;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //walking
        float xInput = Input.GetAxis("Horizontal") * speed;
        float zInput = Input.GetAxis("Vertical") * speed;

        Vector3 move = new Vector3(xInput, 0, zInput);
        move = Vector3.ClampMagnitude(move, speed);
        move = transform.TransformVector(move);
        //jumping
        if (cc.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = 8f;
            }
            else
            {
                ySpeed = gravity * Time.deltaTime;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }
        //actual movement
        cc.Move((move + new Vector3(0, ySpeed, 0)) * Time.deltaTime);
        //left right look
        float xMouse = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(0, xMouse, 0);
        //up down look
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, lookMin, lookMax);
        Quaternion camRotation = Quaternion.Euler(pitch, 0, 0);
        fpsCamera.localRotation = camRotation;
	}
}
