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

    private Vector3 defaultPosition;

    void Start()
    {
        defaultPosition = transform.position;
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

        if (StateManager.gameState == StateManager.State.Playing)
            characterController.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);

        velocity.y += gravityScale * Time.deltaTime;

        if (StateManager.gameState == StateManager.State.Playing)
            characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Car hitCar = other.GetComponent<Car>();
        if (hitCar != null)
        {
            hitCar.Beep();
            ResetPosition();
            ScoreController.ResetScoreStatic();
        }
    }

    private void ResetPosition()
    {
        Waver playerWaver = GetComponentInChildren<Waver>();
        if (playerWaver != null) playerWaver.ColorChange(false);
        characterController.enabled = false;
        transform.position = defaultPosition;
        characterController.enabled = true;
    }
}
