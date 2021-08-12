using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    public Collider2D cameraDownTrigger;

    public float cameraDistance = 5f;
    public float cameraFlyDurationUp = 1f;
    public float cameraFlyDurationDown = 0.1f;

    public static bool savePosition = false;

    [HideInInspector]
    public bool moveCameraDown = false;
    
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
        if(moveCameraDown)
        {
            Debug.DrawLine(player.transform.position, player.transform.position + Vector3.down, Color.red);
            float t = (Time.time - startTime) / cameraFlyDurationDown;
            float cameraPosYNew = player.transform.position.y + cameraDistance;
            float cameraPosYActual = Mathf.SmoothStep(cameraPosYOld, cameraPosYNew, t);

            transform.position = new Vector3(transform.position.x, cameraPosYActual, transform.position.z);

            if(animator.GetBool("IsGrounded") && animator.GetFloat("Velocity") == 0)
            {
                moveCameraDown = false;
            }

        }else
        {
            if(animator.GetBool("IsGrounded") && animator.GetFloat("Velocity") < 0.01 && !moveCamera)
            {
                moveCamera = true;
                startTime = Time.time;
                SaveData.Current.SetPlayerPosition(player.transform.position, player.transform.localRotation.y);
                SaveData.Current.SetTimer(LiveTimer.timer);
                SerializationManager.Save(SaveData.Current);
                savePosition = false;
            }
            
            if(moveCamera)
            {
                float t = (Time.time - startTime) / cameraFlyDurationUp;
                float cameraPosYNew = player.transform.position.y + cameraDistance;
                float cameraPosYActual = Mathf.SmoothStep(cameraPosYOld, cameraPosYNew, t);

                if(cameraPosYOld < cameraPosYNew)
                {
                    Debug.DrawLine(player.transform.position, player.transform.position + Vector3.up, Color.green);
                    transform.position = new Vector3(transform.position.x, cameraPosYActual, transform.position.z);
                }

                if(animator.GetBool("IsCrouching"))
                {
                    cameraPosYActual = cameraPosYNew;
                }

                if(cameraPosYActual == cameraPosYNew)
                {
                    transform.position = new Vector3(transform.position.x, cameraPosYNew, transform.position.z);
                    cameraPosYOld = cameraPosYNew;
                    moveCamera = false;
                }
            }
        }
    }
}
