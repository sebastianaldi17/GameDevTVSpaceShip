using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector; // Max movement from inital position
    [SerializeField] float period = 2f; // Time needed for a single cycle

    Vector3 startingPosition;

    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) // Prevent divide by 0 error
        {
            return;
        }
        float cycles = Time.time / period; // increases with time
        float sinValue = Mathf.Sin(cycles * tau); // goes from -1 to 1
        float movementFactor = (sinValue + 1f) / 2f;  // set value to between 0 and 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
