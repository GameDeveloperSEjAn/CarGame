using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public Camera mainCamera;
    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;
    private float minFOV = 60f;
    private float maxFOV = 65f;

    public Rigidbody carRb;
    public AudioSource carAudio;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    public AudioClip startupAudio;

    void Start()
    {
        carAudio = GetComponent<AudioSource>();
        carRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        EngineSound();
        // Clamp the current speed to be within the min and max speed range
        float clampedSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        // Calculate the target FOV based on the current speed
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, (clampedSpeed - minSpeed) / (maxSpeed - minSpeed));

        // Smoothly transition the FOV using Lerp
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime);
    }
    void EngineSound()
    {
        currentSpeed = carRb.velocity.magnitude;
        pitchFromCar = carRb.velocity.magnitude / 50f;

        if (currentSpeed < minSpeed )
        {
            carAudio.pitch = minPitch ;
        }
        if (currentSpeed > minSpeed && currentSpeed < maxSpeed )
        {
            carAudio.pitch = maxPitch + pitchFromCar ;
        }
        if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}