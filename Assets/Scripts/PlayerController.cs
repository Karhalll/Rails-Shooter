using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m^-1")]
    [SerializeField] float controlSpeed = 10f;
    [Tooltip("In m")]
    [SerializeField] float xRange = 6f;
    [Tooltip("In m")]
    [SerializeField] float yRange = 4f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float positionYawFactor = 6f;

    [Header("Controll-throw Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void OnPlayerDeath() //Caled by string refence
    {
        isControlEnabled = false;
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
            AxisClampedMovement(transform.localPosition.x, xThrow, xRange),
            AxisClampedMovement(transform.localPosition.y, yThrow, yRange),
            transform.localPosition.z);
    }

    /// <summary>
    /// Clamping the desired Axis on localPos parameter.
    /// </summary>
    /// <param name="localPos">Targeted axis.</param>
    /// <param name="Throw">Throw from player input.</param>
    private float AxisClampedMovement(float localPos, float Throw, float Range)
    {
        float Offset = Throw * controlSpeed * Time.deltaTime;

        float rawPos = localPos + Offset;
        float clampedPos = Mathf.Clamp(rawPos, -Range, Range);

        return clampedPos;
    }
}
