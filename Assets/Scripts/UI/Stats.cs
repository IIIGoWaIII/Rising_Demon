using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public static int jumpsConut = 0;
    public static int fallsCount = 0;

    public TextMeshProUGUI liveTimer;

    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Time: " + liveTimer.text + 
                        "\nJumps: " + jumpsConut.ToString() +
                        "\nFalls: " + fallsCount.ToString();
    }
}
