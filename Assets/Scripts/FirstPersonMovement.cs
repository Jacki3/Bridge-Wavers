using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 12;

    [SerializeField]
    private float gravityScale = -10;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundDist;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private LayerMask groundMask;

    private bool isGrounded;

    private CharacterController characterController;

    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded =
            Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);

        velocity.y += gravityScale * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
