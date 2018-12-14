using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    // DEFAULT VALUES
    [SerializeField] float rotationForce = 250f;
    [SerializeField] float mainThrust = 50f;
    [SerializeField] float levelLoadDelay = 2f;

    // AUDIO
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip succesSound;
    [SerializeField] AudioClip killSound;

    // PARTICLES
    [SerializeField] ParticleSystem engineParticle;
    [SerializeField] ParticleSystem succesParticle;
    [SerializeField] ParticleSystem killParticle;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Win, Kill, Alive};
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive) // you can control spaceship only if you alive
        {
            ControlRocketThrust();
            ControlRocketRotate();
        }

        SecretKeysInGame();
	}

    private void SecretKeysInGame()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            LoadNextLevel(); 
    }

    // collision detector
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) // stop checking collision when dead or you off collision in secret mode
            return;

        switch (collision.gameObject.tag) // check collision
        {
            case "Friendly": // starting point is friendly
                // do nothing
                break;
            case "Finish": // you land on green Landing Pad
                state = State.Win;
                audioSource.Stop(); // stop engine sound
                audioSource.PlayOneShot(succesSound);
                succesParticle.Play();
                Invoke("LoadNextLevel", levelLoadDelay); // load next scene
                break;
            default: // you hit other elements
                state = State.Kill;
                audioSource.Stop(); // stop engine sound
                engineParticle.Stop();
                audioSource.PlayOneShot(killSound);
                killParticle.Play();
                Invoke("LoadFirstLevel", levelLoadDelay); // load first scene 
                break;
        }
    }

    // load first scene
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    //load next level
    private void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings) // if index nex scene equals number of scenes load first scene
        {
            nextLevelIndex = 1;
        }
        SceneManager.LoadScene(nextLevelIndex); 
    }

    // ship rotation
    private void ControlRocketRotate()
    {
        var rotationThisFrame = rotationForce * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(-rotationThisFrame);
        }
    }

    private void Rotate(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation =false;
    }

    // ship engine thrust
    private void ControlRocketThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying) // play engine sound when click space
                audioSource.PlayOneShot(engineSound);
            engineParticle.Play(); // play engine effect
        }
        else
        {
            // stop particle and audio effect
            audioSource.Stop();
            engineParticle.Stop();
        }
    }
}
