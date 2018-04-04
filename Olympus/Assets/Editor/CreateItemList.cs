using UnityEngine;
using UnityEditor;

public class CreateItemList
{
    [MenuItem("Assets/Create/Inventory Item List")]
    public static ItemList Create()
    {
        ItemList asset = ScriptableObject.CreateInstance<ItemList>();

        AssetDatabase.CreateAsset(asset, "Assets/ItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}