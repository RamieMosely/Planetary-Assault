using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]  [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = .5f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;
    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFactor = -2f;

    [SerializeField] float controlPitchFactor = -12f;

    [SerializeField] float positionYawFactor = -5f;

    [SerializeField] float controlRollFactor = -20f;

    float horizontalThrow;
    float verticalThrow;


    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;
        
        float pitch = pitchDueToControlThrow + pitchDueToPosition;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    void ProcessTranslation()
    {
        horizontalThrow = movement.ReadValue<Vector2>().x;
        verticalThrow = movement.ReadValue<Vector2>().y;


        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

        //Debug.Log(horizontalThrow);
        //Debug.Log(verticalThrow);


        //float horizontalThrow = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalThrow);

        //float verticalThrow = Input.GetAxis("Vertical");
        //Debug.Log(verticalThrow);
    }

    void ProcessFiring()
    {
        bool isShooting = false;

        //if pushing fire button
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true); 
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        //for each of the lasers that we have actaivate them
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }

    }
}
