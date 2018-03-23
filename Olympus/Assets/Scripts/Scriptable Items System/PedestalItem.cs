using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditor;

[System.Serializable]
public class PedestalItem
{
    public string itemName = "New Item";
    [HideInInspector]
    public string[] itemType = new string[] { "Passive", "Active" };
    public int itemTypeIndex = 0;
    public GameObject itemObject = null;   
    public bool hasTagLine = false;
    public string tagLine = "Enter here";
    public float IDValue = 0;
}