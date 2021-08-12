using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    // Vector3(1.475f, -4.681f, 0f);
    public float positionX = 1.475f;
    public float positionY = -4.681f;
    public float positionZ = 0f;
    public float rotationY = 180f;
    public int jumpsCount = 0;
    public int fallsCount = 0;
}
