using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ItemEditor : EditorWindow
{

    public ItemList thisItemList;
    private int viewIndex = 1;

    [MenuItem("Window/Inventory Item Editor %#e")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(ItemEditor));
    }

    void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            thisItemList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(ItemList)) as ItemList;
        }

    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Inventory Item Editor", EditorStyles.boldLabel);
        if (thisItemList != null)
        {
            if (GUILayout.Button("Show Item List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = thisItemList;
            }
        }
        if (GUILayout.Button("Open Item List"))
        {
            OpenItemList();
        }
        if (GUILayout.Button("New Item List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = thisItemList;
        }
        GUILayout.EndHorizontal();

        if (thisItemList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
            {
                CreateNewItemList();
            }
            if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
            {
                OpenItemList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        if (thisItemList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < thisItemList.itemList.Count)
                {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
            {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
            {
                DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            
            if (thisItemList.itemList.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, thisItemList.itemList.Count);
                //Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
                EditorGUILayout.LabelField("of   " + thisItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();                   

                thisItemList.itemList[viewIndex - 1].itemName = EditorGUILayout.TextField("Item Name", thisItemList.itemList[viewIndex - 1].itemName as string);
                thisItemList.itemList[viewIndex - 1].itemTypeIndex = EditorGUILayout.Popup("Item Type", thisItemList.itemList[viewIndex - 1].itemTypeIndex, thisItemList.itemList[viewIndex - 1].itemType);
                thisItemList.itemList[viewIndex - 1].itemObject = EditorGUILayout.ObjectField("Item Object", thisItemList.itemList[viewIndex - 1].itemObject, typeof(GameObject), false) as GameObject;

                GUILayout.Space(10);
                thisItemList.itemList[viewIndex - 1].IDValue = EditorGUILayout.FloatField("ID Value", thisItemList.itemList[viewIndex - 1].IDValue, GUILayout.ExpandWidth(false));
                thisItemList.itemList[viewIndex - 1].hasTagLine = EditorGUILayout.Toggle("Has Tag Line", thisItemList.itemList[viewIndex - 1].hasTagLine, GUILayout.ExpandWidth(false));
                if (thisItemList.itemList[viewIndex - 1].hasTagLine)
                {
                    thisItemList.itemList[viewIndex - 1].tagLine = EditorGUILayout.TextField("Tag Line", thisItemList.itemList[viewIndex - 1].tagLine as string);
                }

                GUILayout.Space(10);
            }
            else
            {
                GUILayout.Label("This Inventory List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(thisItemList);
        }
    }

    void CreateNewItemList()
    {
        // There is no overwrite protection here!
        // There is No "Are you sure you want to overwrite your existing object?" if it exists.
        // This should probably get a string from the user to create a new name and pass it ...
        viewIndex = 1;
        thisItemList = CreateItemList.Create();
        if (thisItemList)
        {
            thisItemList.itemList = new List<PedestalItem>();
            string relPath = AssetDatabase.GetAssetPath(thisItemList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    void OpenItemList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Inventory Item List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            thisItemList = AssetDatabase.LoadAssetAtPath(relPath, typeof(ItemList)) as ItemList;
            if (thisItemList.itemList == null)
                thisItemList.itemList = new List<PedestalItem>();
            if (thisItemList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    void AddItem()
    {
        PedestalItem newItem = new PedestalItem();
        newItem.itemName = "New Item";
        thisItemList.itemList.Add(newItem);
        viewIndex = thisItemList.itemList.Count;
    }

    void DeleteItem(int index)
    {
        thisItemList.itemList.RemoveAt(index);
    }
}