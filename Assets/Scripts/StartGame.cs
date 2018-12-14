using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    // AUDIO
    [SerializeField] AudioClip applause;
    AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.sceneCountInBuildSettings == 7)
        {
            audioSource.PlayOneShot(applause);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
