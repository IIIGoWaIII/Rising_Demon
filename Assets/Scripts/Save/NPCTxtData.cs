using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCTxtData : MonoBehaviour
{
    private int[] NPCTxts = new int[11];

    public void SetNPCTxt(int NPCIndex, int txtCount)
    {
        NPCTxts[NPCIndex] = txtCount;

        PlayerPrefs.SetInt(NPCIndex.ToString() , NPCTxts[NPCIndex]);
        Debug.Log("saved npc index: " + NPCIndex + " txt nr: " + NPCTxts[NPCIndex]);
        PlayerPrefs.Save();
    }

    public int GetNPCTxt(int NPCIndex)
    {
        NPCTxts[NPCIndex] = PlayerPrefs.GetInt(NPCIndex.ToString() , 0);
        Debug.Log("loaded npc index: " + NPCIndex + " txt nr: " + NPCTxts[NPCIndex]);
        return NPCTxts[NPCIndex];
    }
}
