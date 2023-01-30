using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    [SerializeField]
    private float waveRate = 0.5F;

    [SerializeField]
    private Color32 waveColor;

    private float nextWave = 0.0F;

    private CharacterController controller;

    private Vector3 playerVelocity;

    private bool groundedPlayer;

    private float jumpHeight = 1.0f;

    private float gravityValue = -9.81f;

    private Vector3 defaultPos;

    private float defaultYPos;

    private Color32 defaultColor;

    private Material playerMaterial;

    private float t = 0;

    public bool canWave;

    private Detector activeTrigger;

    private void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
        defaultColor = playerMaterial.color;
        defaultPos = transform.position;
        defaultYPos = defaultPos.y;
        controller = gameObject.AddComponent<CharacterController>();
        controller.radius = 0.3f;
        controller.stepOffset = 0.1f;
        controller.enabled = false;
        controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Detector trigger = other.GetComponent<Detector>();
        if (trigger != null)
        {
            if (trigger.IsBridge())
            {
                canWave = true;
                activeTrigger = trigger;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Detector trigger = other.GetComponent<Detector>();
        if (trigger != null)
        {
            if (trigger.IsBridge())
            {
                canWave = false;
                activeTrigger = null;
            }
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextWave)
        {
            Wave();
        }

        if (t < waveRate)
        {
            t += Time.deltaTime / waveRate;
        }

        playerMaterial.color = Color.Lerp(waveColor, defaultColor, t);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (transform.position.y < defaultYPos) transform.position = defaultPos;
    }

    private void Wave()
    {
        nextWave = Time.time + waveRate;
        playerMaterial.color = waveColor;
        t = 0;
        if (activeTrigger != null)
        {
            if (activeTrigger.CarCheck())
            {
                activeTrigger.CarWaved();
                int score = activeTrigger.GetCarScore();

                ScoreController.AddScoreStatic (score);
            }
        }
    }
}
