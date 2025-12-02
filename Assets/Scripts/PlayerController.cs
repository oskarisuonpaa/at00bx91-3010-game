using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float scoreMultiplier = 1f;

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
    }

    void Update()
    {
        // Keyboard input (WASD + Arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveY = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        moveInput = new Vector2(moveX, moveY).normalized;
        moveVelocity = moveInput * moveSpeed;

        // Mouse pointer movement (optional: hold right-click to move)
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - rb.position).normalized;
            moveVelocity = direction * moveSpeed;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    // Speed-boost powerup
    public IEnumerator ActivateSpeedBoost(float percent, float duration)
    {
        if (speedBoostActive) yield break;

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
        if (scoreBoostActive) yield break;

        scoreBoostActive = true;
        scoreMultiplier = multiplier;

        yield return new WaitForSeconds(duration);

        scoreMultiplier = 1f;
        scoreBoostActive = false;
    }
}