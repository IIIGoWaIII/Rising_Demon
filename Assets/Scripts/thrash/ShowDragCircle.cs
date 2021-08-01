using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDragCircle : MonoBehaviour
{
    public Dragger circle;

    private float radius;
    private Vector3 clickedMousePosition;
    private SpriteRenderer spriteRenderer;

    private void Start() 
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.enabled = circle.MouseIsDown;
        transform.localScale = new Vector3(circle.radius/128, circle.radius/128, circle.radius/128);
        radius = circle.radius;
        clickedMousePosition = circle.ClickedMousePosition;
        transform.localPosition = Camera.main.ScreenToWorldPoint(clickedMousePosition) + new Vector3(0,0,10);

    }
}
