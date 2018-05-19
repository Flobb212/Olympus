using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptObj/RoomObj", order = 1)]
public class ScriptableRoom : ScriptableObject
{
    public List<GameObject> roomPrefabs;
    public Vector2 roomPos;
    public string roomType = "norm";
}
