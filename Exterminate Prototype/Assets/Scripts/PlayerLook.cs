using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Shared_Models;

public class PlayerLook : MonoBehaviour
{
    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public ViewSettings viewSettings;
    float yMinClamp = -70f;
    float yMaxClamp = 80f;

    private void Awake()
    {
        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Rotate the Player to look left and right
        newCharacterRotation.y += viewSettings.xSensitivity * mouseX * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        //Rotate the Camera Holder to look up and down
        newCameraRotation.x += viewSettings.ySensitivity * -mouseY * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, yMinClamp, yMaxClamp);

        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }
}
