using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveDirection;

    [Header("Dash Settings")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashTime;
    private float dashCooldownTimer;

    [Header("Mouse Cursor Replacement")]
    public GameObject mouseCursorObject;
    public float orbitDistance = 0.5f;

    private Rigidbody2D rb;
    private Camera cam;

    public bool facingRight = true;

    private SpriteRenderer spriteRender;
    SoundManager soundManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //HandleMovementInput();
        HandleDash();
        UpdateMouseCursor();
    }

    void FixedUpdate()
    {
        // Handle dashing movement
        if (isDashing)
        {
            rb.linearVelocity = moveDirection.normalized * dashSpeed;
            return;
        }

        // Flip based on horizontal movement
        FlipPlayer(moveDirection.x);
        // Regular movement
        rb.linearVelocity = moveDirection.normalized * moveSpeed;

        //if (rb.linearVelocityX > 0 || rb.linearVelocityY > 0)
        //    soundManager.PlayerWalking();
        //else
        //    soundManager.stopWalking();
        anim.SetFloat("Move", moveDirection.magnitude);
    }

    public void HandleMovementInput(InputAction.CallbackContext ctx)
    {
        float moveX = ctx.ReadValue<Vector2>().normalized.x;
        float moveY = ctx.ReadValue<Vector2>().normalized.y;
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void HandleDash()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f && moveDirection != Vector2.zero)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }
    }

    private void FlipPlayer(float h)
    {
        if ((h > 0 && !facingRight) || (h < 0 && facingRight))
        {
            facingRight = !facingRight;
            spriteRender.flipX = !spriteRender.flipX;
        }
    }

    void UpdateMouseCursor()
    {
        if (mouseCursorObject != null)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector3 playerPos = transform.position;
            Vector3 direction = (mouseWorldPos - playerPos).normalized;

            Vector3 orbitPos = playerPos + direction * orbitDistance;
            mouseCursorObject.transform.position = orbitPos;


        }
    }
}