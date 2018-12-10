using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float rotationForce = 150f;
    [SerializeField] float mainThrust = 40f;

    Rigidbody rigidBody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    // collision detector
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("friendly");
                break;
            default:
                print("dead");
                break;
        }
    }

    // ship rotation
    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        var rotationThisFrame = rotationForce * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    // ship engine thrust
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
