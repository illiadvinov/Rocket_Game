using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float MainThrust = 0;
    [SerializeField] float Rotation = 0;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBuster;
    [SerializeField] ParticleSystem leftBuster;
    [SerializeField] ParticleSystem rightBuster;

    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;

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
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotationLeft();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            RotationRight();
        }

        else
        {
            StopRotation();

        }

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBuster.isPlaying)
        {
            mainBuster.Play();
        }
    }

     void StopThrusting()
    {
        audioSource.Stop();
        mainBuster.Stop();
    }

     void RotationRight()
    {
        ApplyRotation(-Rotation);
        if (!leftBuster.isPlaying) leftBuster.Play();
    }

     void RotationLeft()
    {
        ApplyRotation(Rotation);
        if (!rightBuster.isPlaying) rightBuster.Play();
    }
    void StopRotation()
    {
        rightBuster.Stop();
        leftBuster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing Rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // 
    }
}
