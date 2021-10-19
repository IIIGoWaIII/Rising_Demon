using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTxtExitCheck : MonoBehaviour
{
    public bool didExit = false;

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            didExit = true;
        }
    }
}
