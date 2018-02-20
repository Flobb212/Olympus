using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptObj/RoomTest", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public GameObject roomObject;
    public GameObject roomShape;
    public Vector2 roomPos;
    public string roomType = "norm";
    public bool doorTop, doorBottom, doorLeft, doorRight;
}
