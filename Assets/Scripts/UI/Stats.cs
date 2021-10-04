using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public static int jumpsCount = 0;
    public static int fallsCount = 0;

    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>(); 
        // SaveData.Current.OnLoadGame();
        // jumpsCount = SaveData.Current.GetJumpsCount();
        // fallsCount = SaveData.Current.GetFallsCount();
    }

    // Update is called once per frame
    void Update()
    {

        textMesh.text = "Time: " + LiveTimer.text + 
                        "\nJumps: " + jumpsCount.ToString() +
                        "\nFalls: " + fallsCount.ToString();
    }
}
