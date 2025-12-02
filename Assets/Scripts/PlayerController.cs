using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float scoreMultiplier = 1f;

    [Header("Camera Zoom Settings")]
    public Camera playerCamera;
    public float baseCameraSize = 20f;
    public float cameraZoomMultiplier = 2f;

    [Header("Speed Adjustment Settings")]
    public float speedDecayRate = 0.3f; // How much speed decreases with size
    public float minSpeed = 2f; // Minimum movement speed

    private float originalSpeed;
    private bool speedBoostActive = false;
    private bool scoreBoostActive = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    void Start()
    {
        originalSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        // Auto-find camera if not assigned
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // Update camera zoom based on player size
        UpdateCameraZoom();

        // Calculate current speed based on size
        float currentSpeed = CalculateSpeedBasedOnSize();

        // Keyboard input (WASD + Arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveY = Input.GetAxisRaw("Vertical"); // W/S or Up/Down
        moveInput = new Vector2(moveX, moveY).normalized;
        moveVelocity = moveInput * currentSpeed;

        // Mouse pointer movement (optional: hold right-click to move)
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - rb.position).normalized;
            moveVelocity = direction * currentSpeed;
        }
    }

    void UpdateCameraZoom()
    {
        if (playerCamera != null)
        {
            // Zoom out as player grows
            float targetSize = baseCameraSize + (transform.localScale.x * cameraZoomMultiplier);
            playerCamera.orthographicSize = Mathf.Lerp(
                playerCamera.orthographicSize,
                targetSize,
                Time.deltaTime * 2f
            );
        }
    }

    float CalculateSpeedBasedOnSize()
    {
        // Player slows down as they grow larger
        float sizeScale = transform.localScale.x;
        float calculatedSpeed = moveSpeed / (1f + (sizeScale - 1f) * speedDecayRate);
        return Mathf.Max(calculatedSpeed, minSpeed);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    // Speed-boost powerup
    public IEnumerator ActivateSpeedBoost(float percent, float duration)
    {
        if (speedBoostActive)
            yield break;

        speedBoostActive = true;

        float originalSpeed = moveSpeed;
        moveSpeed += moveSpeed * percent;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        speedBoostActive = false;
    }

    // Score multiplier powerup
    public IEnumerator ActivateScoreMultiplier(float multiplier, float duration)
    {
        if (scoreBoostActive)
            yield break;

        scoreBoostActive = true;
        scoreMultiplier = multiplier;

        yield return new WaitForSeconds(duration);

        scoreMultiplier = 1f;
        scoreBoostActive = false;
    }
}
