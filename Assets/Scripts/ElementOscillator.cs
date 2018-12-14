using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ElementOscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(15f,0f,0f); // vectors x,y,z - directions to move object
    [SerializeField] float period = 2.5f;

    float movementFactor; // range of movements. 0 for not moved and 1 for fully moved 
    Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = transform.position; // starting object position
	}
	
	// Update is called once per frame
	void Update () {
        if (period <= Mathf.Epsilon) { return; } // protect against period is zero
        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2f; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
