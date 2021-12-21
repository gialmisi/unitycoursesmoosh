using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust = 80.0f;
    [SerializeField] float rotationSpeed = 80.0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticle.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Time.deltaTime * thrust * Vector3.up);
        mainThrustParticle.Play();
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void ProcessRotation()
    {
        // turning left
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        // turning right
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StopRotating()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so that we can manually rotate
        transform.Rotate(Time.deltaTime * rotationThisFrame * Vector3.forward);
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
