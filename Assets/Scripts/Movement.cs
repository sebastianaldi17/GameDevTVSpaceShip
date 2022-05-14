using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrustForce = 1000f;
    [SerializeField] float rotationSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        ProcessThrusters();
        ProcessRotation();
    }

    void ProcessThrusters()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        } else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-1);
        }
    }

    private void ApplyRotation(int direction)
    {
        rb.freezeRotation = true; // manually rotate rocket
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * direction);
        rb.freezeRotation = false;
    }
}
