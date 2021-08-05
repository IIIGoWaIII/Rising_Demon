using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlingshot : MonoBehaviour
{
    public CameraPositionController cameraMove;
    public Collider2D colliderJump;
    public Collider2D colliderFall;
    public ParticleSystem jumpDust;
    public ParticleSystem gigafallDust;

    [Range (0.0f, 10.0f)]
    public float power = 10f;

    [Range (0.0f, 10.0f)]
    public float maxDrag = 5f;

    private bool isDraggable = false;
    public bool IsDraggable
    {
        get
        {
            return isDraggable;
        }
    }

    private bool gigajumpDust = false;
    private bool startedDragging = false;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private Animator animator;
    public Animator Animator
    {
        get
        {
            return animator;
        }
    }
    private Vector3 dragStartPos;
    private Touch touch;
    private AudioSource jumpSound;

    private void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        lr = gameObject.GetComponent<LineRenderer>();
        animator = gameObject.GetComponent<Animator>();
        lr.positionCount = 0;
        jumpSound = gameObject.GetComponent<AudioSource>();
    }

    private void Update() 
    {

        if (Input.touchCount > 0 && isDraggable)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }
        animator.SetFloat("Velocity", rb.velocity.magnitude);
        animator.SetFloat("VelocityY", rb.velocity.y);

        if(rb.velocity.y > 0.1)
        {
            colliderJump.enabled = true;
            colliderFall.enabled = false;
        }else
        {
            colliderJump.enabled = false;
            colliderFall.enabled = true;
        }

        if(rb.velocity.y < -20)
        {
            animator.SetBool("Gigafall", true);
            gigajumpDust = true;
        }
        
        if(animator.GetBool("Gigafall") && animator.GetBool("IsGrounded") && gigajumpDust && rb.velocity.magnitude < 0.1f)
        {
            FindObjectOfType<AudioManager>().Play("Gigafall");
            gigafallDust.Play();
            gigajumpDust = false;
        }
    }

    private void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1; 
        lr.SetPosition(0, dragStartPos);
        startedDragging = true;
        animator.SetBool("IsCrouching", true);
        animator.SetBool("Gigafall", false);
    }

    private void Dragging()
    {     
        if(startedDragging)
        {
            Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
            draggingPos.z = 0f;
            lr.positionCount = 2;

            Vector3 dir = draggingPos - dragStartPos;
            float dist = Mathf.Clamp(Vector3.Distance(dragStartPos, draggingPos), 0, maxDrag);
            draggingPos = dragStartPos + (dir.normalized * dist);

            lr.SetPosition(1, draggingPos);

            if(dragStartPos.x > draggingPos.x)
            {
                transform.localRotation = Quaternion.Euler(0f,180f,0f);
            } else
            {
                transform.localRotation = Quaternion.Euler(0f,0f,0f);
            }

        }
    }

    private void DragRelease()
    {
        if(startedDragging)
        {
            lr.positionCount = 0;

            Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
            dragReleasePos.z = 0f;

            Vector3 force = dragStartPos - dragReleasePos;
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

            FindObjectOfType<AudioManager>().Play("Jump");

            rb.AddForce(clampedForce, ForceMode2D.Impulse);
            startedDragging = false;
            animator.SetBool("IsCrouching", false);

            if(clampedForce.magnitude > 10)
            {
                jumpDust.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "MainCamera")
        {
            cameraMove.moveCameraDown = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            isDraggable = true;
            animator.SetBool("IsGrounded", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            FindObjectOfType<AudioManager>().Play("Collision");
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            isDraggable = false;
            animator.SetBool("IsGrounded", false);
        }
    }
}