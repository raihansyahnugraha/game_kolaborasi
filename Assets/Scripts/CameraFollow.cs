using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    public Transform playerPosition;
    public Vector3 offset = Vector3.zero;
    public float smoothSpeed = 5;

    public float mouseSensitivity = 100;
    public float minYAngle = -30;
    public float maxYAngle = 60;

    private float rotationX = 0;
    private float rotationY = 0;

    private void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);
        
        var rotation = Quaternion.Euler(rotationX,rotationY,0);
        var targetPosition = playerPosition.position + rotation * offset;
        
        transform.position = Vector3.Lerp(transform.position,targetPosition,smoothSpeed * Time.deltaTime);
        transform.LookAt(playerPosition);
    }
}
