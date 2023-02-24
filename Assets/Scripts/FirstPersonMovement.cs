using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 12;
    [Header("Feedbacks")]

    [SerializeField]
    private MMFeedbacks dashFeedbacks;

    [Header("Jump Settings")]

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

    [Header("Dash Settings")]

    [SerializeField]
    private float dashTime;
    [SerializeField]
    private float dashSpeed;


    private bool isGrounded;

    private CharacterController characterController;

    private Vector3 velocity;

    private Vector3 defaultPosition;

    private Vector3 move;


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

        move = transform.right * x + transform.forward * z;

        if (StateManager.gameState == StateManager.State.Playing)
        {
            if (Input.GetButtonDown("Dash") && move != Vector3.zero)
            {
                StartCoroutine(DashCoroutine());
            }
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);

        velocity.y += gravityScale * Time.deltaTime;

        if (StateManager.gameState == StateManager.State.Playing)
            characterController.Move(velocity * Time.deltaTime);
    }

    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        dashFeedbacks?.PlayFeedbacks();
        while (Time.time < startTime + dashTime)
        {
            characterController.Move(move * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
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
        playerWaver?.ColorChange(false);
        characterController.enabled = false;
        transform.position = defaultPosition;
        characterController.enabled = true;
    }
}
