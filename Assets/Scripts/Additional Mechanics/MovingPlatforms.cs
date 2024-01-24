using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    public float triggerVelocity = 10f;

    private bool movingTowardsB = true;
    private bool isMoving = false;
    private bool hasPlayerTriggeredMovement = false;
    private Rigidbody2D playerRigidbody2D;

    void Start()
    {
        // Find the player's Rigidbody2D component
        playerRigidbody2D = FindObjectOfType<Rigidbody2D>();

        // Check if the player's Rigidbody2D was found
        if (playerRigidbody2D == null)
        {
            Debug.LogError("Player Rigidbody2D not found. Make sure the player GameObject has a Rigidbody2D component.");
        }
    }

    void Update()
    {
        // Check if the player's rigid body velocity is above the trigger velocity
        if (playerRigidbody2D != null && playerRigidbody2D.velocity.magnitude > triggerVelocity && !hasPlayerTriggeredMovement)
        {
            // Trigger platform movement
            isMoving = true;
        }

        // Move the platform if it's triggered to move
        if (isMoving)
        {
            MovePlatform();
        }

        if(Mathf.Approximately(playerRigidbody2D.velocity.magnitude, 0f))
        {
            hasPlayerTriggeredMovement = false;
        }
    }

    void MovePlatform()
    {
        hasPlayerTriggeredMovement = true;
        
        // Calculate the new position of the platform
        Vector3 targetPosition = movingTowardsB ? pointB.position : pointA.position;

        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the platform has reached the target position within a small threshold
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // If so, switch direction and stop moving
            movingTowardsB = !movingTowardsB;
            isMoving = false;
        }
    }
}
