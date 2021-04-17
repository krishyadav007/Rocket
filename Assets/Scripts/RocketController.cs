using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
	[SerializeField] float rcsThrust = 100f;
    [SerializeField] float shipThrust = 100f;

	[SerializeField] private ParticleSystem fr;

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody>();

		fr = GetComponent<ParticleSystem>();
	}
	
	// FixedUpdate is called every 0.02 sec
	void FixedUpdate () {

        Thrust();
        Rotate();		
	}

    private void Thrust()
    {
    
        // We calculate the thrust for this frame by multiplying our scalar shipThrust
        // by the change in time from the previous frame. Time.deltaTime is frame independent
        // because it tells us the time since the last time the function was called.
        // Thus, longer frames = more movement, just like we want
        float thrustThisFrame = shipThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            // We scale up our force by the thrust for the frame
            // print("Thrusting");
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            fr.Emit(3);

            // fr.Stop();
            // fr.Play();
        } else {
            // fr.enabled = false;
        }

    }

    private void Rotate()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation

        // We do the same thing as in thrust
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            print("Rotating left");
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            print("Rotating right");
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            print("Rotating right");
            transform.Rotate(Vector3.right * rotationThisFrame);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            print("Rotating right");
            transform.Rotate(Vector3.left * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control

    }


}