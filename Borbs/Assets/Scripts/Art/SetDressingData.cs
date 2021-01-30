using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(menuName = "HaH/SetdressingData" , order = 100)]

public class SetDressingData : ScriptableObject
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public float Desity;

    public float yRanRotation = 360;
    public float xRanRotation = 7;
    public float zRanRotation = 7;

    public float minScale = 0.8f;
    public float maxScale = 1f;
}
