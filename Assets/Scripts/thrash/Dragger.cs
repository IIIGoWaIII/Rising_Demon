using UnityEngine;

/// <summary>
/// Drag a Rigidbody2D by selecting one of its colliders by pressing the mouse down.
/// When the collider is selected, add a TargetJoint2D.
/// While the mouse is moving, continually set the target to the mouse position.
/// When the mouse is released, the TargetJoint2D is deleted.`
/// </summary>

public class Dragger : MonoBehaviour
{
	public LayerMask dragLayers;

	public Collider2D playerColider;

	public LimitVelocity canBeGrabbed;

	[Range (0.0f, 100.0f)]
	public float damping = 1.0f;

	[Range (0.0f, 1000.0f)]
	public float frequency = 200.0f;

	[Range (0.0f, 100.0f)]
	public float radius = 50.0f;

	public bool drawDragLine = true;
	public Color color = Color.cyan;

	private bool mouseIsDown = false;
	public bool MouseIsDown
	{
		get
		{
			return mouseIsDown;
		}
	}

	private float mouseDistance = 0f;
	private Vector2 clickedMousePosition;
	public Vector2 ClickedMousePosition
	{
		get
		{
			return clickedMousePosition;
		}
	}

	private TargetJoint2D targetJoint;

	void Update ()
	{
		// Calculate the world position for the mouse.
		var worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Input.GetMouseButtonDown (0) && canBeGrabbed.CanBeGrabbed)
		{

			// Fetch the first collider.
			// NOTE: We could do this for multiple colliders.
			var collider = Physics2D.OverlapPoint (worldPos, dragLayers);
			if (!collider)
				return;

			// Fetch the collider body.
			var body = collider.attachedRigidbody;
			if (!body)
				return;

			// Add a target joint to the Rigidbody2D GameObject.
			targetJoint = body.gameObject.AddComponent<TargetJoint2D> ();
			targetJoint.dampingRatio = damping;
			targetJoint.frequency = frequency;

			// Attach the anchor to the local-point where we clicked.
			targetJoint.anchor = targetJoint.transform.InverseTransformPoint (worldPos);	

			mouseIsDown = true;
			clickedMousePosition = Input.mousePosition;
		
		}


		if (mouseIsDown)
		{
			Vector2 mousePosition = Input.mousePosition;
			Vector2 center = mousePosition;
	
			//direction from Center to Cursor
			Vector2 direction = mousePosition - center; 
			Vector2 normalizedDirection = direction.normalized;

			mouseDistance = Vector2.Distance(mousePosition, clickedMousePosition);

			print("mouse position: " + mousePosition); 
			print("clicked mouse position: " + clickedMousePosition);
			print("distance: " + mouseDistance);

			if (mouseDistance >= radius)
			{
				stopMouseDragging();
			}
			else if (Input.GetMouseButtonUp (0))
			{
				stopMouseDragging();
			}
		}

		// Update the joint target.
		if (targetJoint)
		{
			targetJoint.target = worldPos;

			// Draw the line between the target and the joint anchor.
			if (drawDragLine)
				Debug.DrawLine (targetJoint.transform.TransformPoint (targetJoint.anchor), worldPos, color);
		}
	}

	private void stopMouseDragging()
	{
		// Send feedback and destroy puller
		Destroy (targetJoint);
		targetJoint = null;
		mouseIsDown = false;
		canBeGrabbed.CanBeGrabbed = false;
		return;
	}

}
