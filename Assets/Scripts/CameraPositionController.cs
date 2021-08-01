using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    public float cameraDistance = 5f;
    public float cameraFlyDuration = 1f;

    private bool moveCamera = false;
    private float startTime;
    private float cameraPosYOld;
    private GameObject player;
    private Animator animator;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();

        transform.position = new Vector3(transform.position.x, player.transform.position.y + cameraDistance, transform.position.z);
        cameraPosYOld = player.transform.position.y + cameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("IsGrounded") && animator.GetFloat("Velocity") <= 0 && !moveCamera)
        {
            moveCamera = true;
            startTime = Time.time;
        }

        if(moveCamera == true)
        {
            float t = (Time.time - startTime) / cameraFlyDuration;
            float cameraPosYNew = player.transform.position.y + cameraDistance;
            float cameraPosYActual = Mathf.SmoothStep(cameraPosYOld, cameraPosYNew, t);

            transform.position = new Vector3(transform.position.x, cameraPosYActual, transform.position.z);

            if(cameraPosYActual == cameraPosYNew)
            {
                cameraPosYOld = cameraPosYNew;
                moveCamera = false;
            }
        }

    }
}
