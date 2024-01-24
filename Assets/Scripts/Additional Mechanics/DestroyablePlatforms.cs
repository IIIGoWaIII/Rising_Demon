using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatforms : MonoBehaviour
{

    public int hitPoints = 3;
    public BoxCollider2D[] boxColliders2d;

    private SpriteRenderer spriteRenderer;

    private int hits = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && other.relativeVelocity.magnitude > 20.5f)
        {
            hits++;
            if(hits == hitPoints)
            {
                foreach (BoxCollider2D collider in boxColliders2d)
                {
                    collider.enabled = false;
                }
                Invoke("EnablePlatrorm",1f);
            }
            spriteRenderer.color = new Color(1f, 1f, 1f, (1-(float)hits/(float)hitPoints));
        }
    }

    private void EnablePlatrorm()
    {
        foreach (BoxCollider2D collider in boxColliders2d)
        {
            collider.enabled = true;
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        hits = 0;
    }
}
