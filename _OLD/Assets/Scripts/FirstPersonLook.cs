using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Unity.Mathematics;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    private float sens = 100f;

    [SerializeField]
    private float zoomFOV = 50f;

    [SerializeField]
    private Transform playerBod;

    private float xRot = 0;

    public Camera playerCam;

    private float defaultFOV;

    void Start()
    {
        defaultFOV = playerCam.fieldOfView;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        if (StateManager.gameState == StateManager.State.Playing)
        {
            Cursor.lockState = CursorLockMode.Locked;

            xRot += mouseY;
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            playerBod.Rotate(Vector3.up * mouseX);
        }
        else
            Cursor.lockState = CursorLockMode.None;



    }
}
