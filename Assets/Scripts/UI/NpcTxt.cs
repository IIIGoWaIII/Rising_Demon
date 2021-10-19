using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcTxt : MonoBehaviour
{

    public int indexNPC = 0;
    public NpcTxtExitCheck exitCheck;
    public string[] texts;

    private BoxCollider2D boxCollider2D;
    private TextMeshPro txt;

    private int collisionCount = -1;
    private float time = 0f;
    private float timeBigegr = 0f;
    private int counter = 0;
    private bool sayLine = false;
    private bool firstInteraction = true;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        txt = gameObject.GetComponent<TextMeshPro>();
        txt.enabled = false;
        SaveData.Current.OnLoadGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sayLine)
        {
            SayLine();
        }
        else
        {
            txt.enabled = false;
        }
     
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if((!sayLine && collisionCount < texts.Length && exitCheck.didExit) || firstInteraction)
            {
                collisionCount++;
                sayLine = true;
                counter = 0;
                firstInteraction = false;

                // save collision count

            } else if (collisionCount > texts.Length)
            {
                collisionCount = texts.Length;
            }
        }
    }

    private void SayLine()
    {
        txt.enabled = true;
        txt.text = texts[collisionCount];

        int totalVisibleCharacters = txt.textInfo.characterCount;
        int visibleCount = counter % (totalVisibleCharacters + 1);

        txt.maxVisibleCharacters = visibleCount;

        time = time + 1f * Time.deltaTime;

        if (time >= 0.05f)
        {
            counter++;
            time = 0f;
        }
        
        if (totalVisibleCharacters == visibleCount)
        {  
            timeBigegr = timeBigegr + 1f * Time.deltaTime;
            counter = totalVisibleCharacters;
            if (timeBigegr >= 5f)
            {
                sayLine = false;
                timeBigegr = 0f;
                exitCheck.didExit = false;
            }
        } 
    }

    public void collisionCountReset()
    {
        collisionCount = 0;
    }
}

