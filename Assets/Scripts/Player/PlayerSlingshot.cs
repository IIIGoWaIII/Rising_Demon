using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerSlingshot : MonoBehaviour
{
    // Public variables for settings, camera control, colliders, particle systems, and sprite shape controller
    public SettingsMenu settingsMenu;
    public CameraPositionController cameraMove;
    public Collider2D colliderJump;
    public Collider2D colliderFall;
    public ParticleSystem jumpDust;
    public ParticleSystem gigafallDust;
    public SpriteShapeController jumpPower;

    // Configurable variables for power and maximum drag
    [Range(0.0f, 10.0f)]
    public float power = 10f;

    [Range(0.0f, 10.0f)]
    public float maxDrag = 5f;

    // Private variables to manage state and references
    private bool isDraggable = false;
    public bool IsDraggable => isDraggable;

    private Vector3 draggingPos;
    private bool gigajumpDust = false;
    private bool startedDragging = false;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private Animator animator;
    public Animator Animator => animator;
    private Vector3 dragStartPos;
    private AudioSource jumpSound;

    // Initialization
    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();

        // Set initial line renderer position count to 0
        lr.positionCount = 0;

        // Load game data and settings
        SaveData.Current.OnLoadGame();
        SaveData.Current.GetPlayerPosition(gameObject);
        settingsMenu.LoadSettings();
        Stats.jumpsCount = SaveData.Current.GetJumpsCount();
        Stats.fallsCount = SaveData.Current.GetFallsCount();

        // Set initial scale of jump power indicator to 0
        jumpPower.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Update method called once per frame
    private void Update() 
    {
        HandleTouchInput();
        // HandleMouseInput();

        // Update animator parameters
        animator.SetFloat("Velocity", rb.velocity.magnitude);
        animator.SetFloat("VelocityY", rb.velocity.y);

        // Check for gigafall condition
        if (rb.velocity.y < -20)
        {
            animator.SetBool("Gigafall", true);
            gigajumpDust = true;
        }

        // Play gigafall effects when conditions are met
        if (animator.GetBool("Gigafall") && animator.GetBool("IsGrounded") && gigajumpDust && rb.velocity.magnitude < 0.1f)
        {
            FindObjectOfType<AudioManager>().Play("Gigafall");
            gigafallDust.Play();
            gigajumpDust = false;
            Stats.fallsCount++;
            SaveData.Current.SetFallsCount(Stats.fallsCount);
            SerializationManager.Save(SaveData.Current);
        }
    }

    // Handle touch input for drag operations
    private void HandleTouchInput()
    {
        if (Input.touchCount > 0 && isDraggable)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    DragStart(touch.position);
                    break;
                case TouchPhase.Moved:
                    Dragging(touch.position);
                    break;
                case TouchPhase.Ended:
                    DragRelease(touch.position);
                    break;
            }
        }
    }

    // Handle mouse input for drag operations
    private void HandleMouseInput()
    {
        if (isDraggable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DragStart(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Dragging(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                DragRelease(Input.mousePosition);
            }
        }
    }

    // Start drag operation
    private void DragStart(Vector3 inputPosition)
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(inputPosition);
        dragStartPos.z = 0f;
        lr.positionCount = 1; 
        lr.SetPosition(0, dragStartPos);
        startedDragging = true;
        animator.SetBool("IsCrouching", true);
        animator.SetBool("Gigafall", false);
    }

    // Handle dragging operation
    private void Dragging(Vector3 inputPosition)
    {     
        if (startedDragging)
        {
            draggingPos = Camera.main.ScreenToWorldPoint(inputPosition);
            draggingPos.z = 0f;
            lr.positionCount = 2;

            Vector3 dir = draggingPos - dragStartPos;
            float dist = Mathf.Clamp(Vector3.Distance(dragStartPos, draggingPos), 0, maxDrag);
            draggingPos = dragStartPos + (dir.normalized * dist);

            // Set LineRenderer positions
            lr.SetPosition(0, dragStartPos);
            lr.SetPosition(1, draggingPos);

            jumpPower.transform.localScale = new Vector3(1f, 1f, 1f);
            jumpPower.spline.SetPosition(1, dragStartPos);
            jumpPower.spline.SetPosition(0, draggingPos);

            Debug.DrawLine(jumpPower.spline.GetPosition(0), jumpPower.spline.GetPosition(1), Color.blue);

            // Update player rotation based on dragging direction
            transform.localRotation = dragStartPos.x > draggingPos.x ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        }
    }

    // Release drag operation
    private void DragRelease(Vector3 inputPosition)
    {
        if (startedDragging && dragStartPos.y > draggingPos.y)
        {
            lr.positionCount = 0;

            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(inputPosition);
            dragReleasePos.z = 0f;

            Vector3 force = dragStartPos - dragReleasePos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

            FindObjectOfType<AudioManager>().Play("Jump");

            rb.AddForce(clampedForce, ForceMode2D.Impulse);
            startedDragging = false;
            animator.SetBool("IsCrouching", false);

            Stats.jumpsCount++;
            SaveData.Current.SetJumpsCount(Stats.jumpsCount);
            SerializationManager.Save(SaveData.Current);

            if (clampedForce.magnitude > 10)
            {
                jumpDust.Play();
            }

            if (!LiveTimer.timerTicking)
            {
                LiveTimer.timerTicking = true;
                LiveTimer.startTime = Time.time;
            }
        }
        else
        {
            animator.SetBool("IsCrouching", false);
        }
        CameraPositionController.savePosition = true;
        jumpPower.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Trigger event for camera movement
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            cameraMove.moveCameraDown = true;
        }
    }

    // Collision stay event to enable dragging
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isDraggable = true;
            animator.SetBool("IsGrounded", true);
        }
    }

    // Collision enter event for wall collision sound
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            FindObjectOfType<AudioManager>().Play("Collision");
        }
    }

    // Collision exit event to disable dragging
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isDraggable = false;
            animator.SetBool("IsGrounded", false);
        }
    }
}
