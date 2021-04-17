using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool shouldRotate = true;

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    float wantedRotationAngle;
    float wantedHeight;
    float currentRotationAngle;
    float currentHeight;
    Quaternion currentRotation;

    void LateUpdate ()
    {
       if (target){
           // Calculate the current rotation angles
           wantedRotationAngle = target.eulerAngles.y;
           wantedHeight = target.position.y + height;
           currentRotationAngle = transform.eulerAngles.y;
           currentHeight = transform.position.y;
           // Damp the rotation around the y-axis
           currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
           // Damp the height
           currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
           // Convert the angle into a rotation
           currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
           // Set the position of the camera on the x-z plane to:
           // distance meters behind the target
           transform.position = target.position;
           transform.position -= currentRotation * Vector3.forward * distance;
           // Set the height of the camera
           transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);
           // Always look at the target
           if (shouldRotate)
               transform.LookAt (target);
       }

    }

    public float sensitivity = 0.5f;
    // public Transform target;
     void FixedUpdate ()
     {
         float rotateHorizontal = Input.GetAxis ("Mouse X");
         float rotateVertical = Input.GetAxis ("Mouse Y");
         transform.RotateAround (target.position, transform.up, rotateHorizontal * sensitivity);
         // transform.Rotate(-transform.up * rotateHorizontal * sensitivity); //use instead if you dont want the camera to rotate around the player
         transform.RotateAround (Vector3.zero, transform.right, rotateVertical * sensitivity);
     }
}
