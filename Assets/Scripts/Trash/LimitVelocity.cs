using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVelocity : MonoBehaviour
{

    public float maxVelocity = 10f;

    private Rigidbody2D rb;

    private bool canBeGrabbed = true;
    public bool CanBeGrabbed
    {   
        get
        {
            return canBeGrabbed;
        }
        set
        {
            canBeGrabbed = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() 
    {
        if(rb.velocity.magnitude >= maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    	void OnCollisionStay2D(Collision2D col)
	{
		if(col.gameObject.tag == "Ground")
		{
			canBeGrabbed = true;
		}
	}

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.tag == "Ground")
		{
			rb.velocity = new Vector3(0,0,0);
		}
    }
}
