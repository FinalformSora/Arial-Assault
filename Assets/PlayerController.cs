using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 8f;
    [Tooltip("In m")] [SerializeField] float xRange = 9f;
    [Tooltip("In m")] [SerializeField] float yRange = 4f;

    [Header("Screen Position")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 4f;

    [Header("Screen Control")]
    [SerializeField] float controlPitchFactor = -25f;
    [SerializeField] float controlRollFactor = -30f;

    bool isControlEnabled = true;
    // Update is called once per frame
    void Update()
    {
       if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
       
    }
    void OnPlayerDeath()
    {
        print("Death");
        isControlEnabled = false;
    }

    void ProcessRotation()
    {

        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void ProcessTranslation()
    {
        //  float xThrow = Input.GetAxisRaw("Horizontal");

        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        //-6.25 && 6.05
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        Vector3 stats = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        transform.localPosition = stats;
    }
}
