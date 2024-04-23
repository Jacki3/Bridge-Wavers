using System.Collections;
using UnityEngine;
using MoreMountains.Feedbacks;
public class FirstPersonMovement : MonoBehaviour
{
    #region serialized vars
    [SerializeField]
    protected string playerName;

    [SerializeField]
    private float moveSpeed = 12;
    [SerializeField]
    private float waveRate;
    [Header("Feedbacks")]
    [SerializeField]
    private MMFeedbacks dashFeedbacks;
    [SerializeField]
    private MMFeedbacks waveFeedback;
    [SerializeField]
    private MMFeedbacks hitFeedback;
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
    #endregion

    #region private vars
    private bool isGrounded;
    private CharacterController characterController;
    private Vector3 velocity;
    private Vector3 defaultPosition;
    private Vector3 move;
    private bool canWave = false;
    private float nextWave;
    #endregion

    #region static vars
    public delegate void WaveHandler(string name);
    public static event WaveHandler wave;
    #endregion

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

        if (Input.GetButtonUp("Wave") && Time.time > nextWave && StateManager.gameState == StateManager.State.Playing)
        {
            Wave();
        }
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
        //Player was hit by an oncoming car
        Car hitCar = other.GetComponent<Car>();
        if (hitCar != null)
        {
            hitCar.Beep();
            hitFeedback?.PlayFeedbacks();
            ResetPosition();
            ScoreController.ResetScoreStatic(); //Resetting score seems a bit too harsh; should be a multiplier
        }

        //Player is a wave zone
        WaveOperatedZone waveZone = other.GetComponent<WaveOperatedZone>();
        if (waveZone != null) 
        {
            waveZone.SetPlayerName(playerName); //we should also highlight the zone to show we are in a wave operated zone (an update method on the zone would work well)
            canWave = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Player has now left a wave operated zone
        WaveOperatedZone waveZone = other.GetComponent<WaveOperatedZone>();
        if (waveZone != null)
        {
            waveZone.SetPlayerName(string.Empty);
            canWave = false;
        }
    }

    private void Wave()
    {
        if(wave != null)
        {
            if(canWave)
            { 
                wave(playerName); 
            }
        }
        waveFeedback?.PlayFeedbacks();
        nextWave = Time.time + waveRate;
    }

    private void ResetPosition()
    {
        //Force player to not move so we can reset their position to the original starting place
        characterController.enabled = false;
        transform.position = defaultPosition;
        characterController.enabled = true;
    }
}
