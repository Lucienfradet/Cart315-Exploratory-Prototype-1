using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 1;

    [Header("Running")]
    public bool canRun = false;
    public bool IsRunning { get; private set; }
    public float runSpeed = 1;
    public KeyCode runningKey = KeyCode.LeftShift;

    public AudioSource audio;
    public AudioClip woop;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        DrunkMovement();
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    Boolean drunkSwitch = false;
    Boolean drunkProgSwitch = false;
    Vector2 direction;
    int drunkForceMag = 0;
    int drunkForceMax = 10;
    void DrunkMovement()
    {
        if (!drunkSwitch)
        {
            System.Random rand = new System.Random();
            double r = rand.NextDouble();
            if (r < 0.01)
            {
                drunkSwitch = true;
                direction = RandomVector2(0.0f, Math.PI * 2);
                audio.PlayOneShot(woop);
            }
        }
        else
        {
            Vector3 drunkPush = new Vector3(direction.x, 0.0f, direction.y);
            drunkPush *= drunkForceMag;
            rigidbody.AddForce(drunkPush, ForceMode.Impulse);

            if (drunkForceMag < drunkForceMax && !drunkProgSwitch)
            {
                drunkForceMag++;
            }
            else
            {
                drunkProgSwitch = true;
                drunkForceMag--;
            }

            if (drunkForceMag < 0 && drunkProgSwitch)
            {
                drunkForceMag = 0;
                drunkSwitch = false;
                drunkProgSwitch = false;
            }
        }
        
        

         
    }

    public Vector2 RandomVector2(double angleMin, double angleMax)
    {
        //System.Random rand = new System.Random();
        double random = UnityEngine.Random.value * angleMax + angleMin;
        return new Vector2(Mathf.Cos((float)random), Mathf.Sin((float)random));
    }
}