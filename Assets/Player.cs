using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In m^-1")]
    [SerializeField] float xSpeed = 10f;
    [Tooltip("In m")]
    [SerializeField] float xRange = 6.3f;
    [Tooltip("In m^-1")]
    [SerializeField] float ySpeed = 10f;
    [Tooltip("In m")]
    [SerializeField] float yRange = 4f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;

    [SerializeField] float positionYawFactor = 2f;

    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    void Start()
    {
        
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControllThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControllThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        transform.localPosition = new Vector3(
            AxisClampedMovement(transform.localPosition.x, xThrow, xSpeed, xRange),
            AxisClampedMovement(transform.localPosition.y, yThrow, ySpeed, yRange),
            transform.localPosition.z);
    }

    /// <summary>
    /// Clamping the desired Axis on localPos parameter.
    /// </summary>
    /// <param name="localPos">Targeted axis.</param>
    /// <param name="Throw">Throw from player input.</param>
    private float AxisClampedMovement(float localPos, float Throw, float Speed, float Range)
    {
        float Offset = Throw * Speed * Time.deltaTime;

        float rawPos = localPos + Offset;
        float clampedPos = Mathf.Clamp(rawPos, -Range, Range);

        return clampedPos;
    }
}
