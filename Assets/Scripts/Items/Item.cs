using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Item",fileName = "New Item")]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public ItemType defaultType;

    public ItemFlags possibleStateFlags;
    
    public Material itemMaterial;
    public Mesh solidMesh;


    public static bool CheckAllow(Item item,ItemType target){
        switch (target)
        {
            case ItemType.gas:
                return item.possibleStateFlags.gasAllowed;
            case ItemType.liquid:
                return item.possibleStateFlags.liquidAllowed;
            case ItemType.solid:
                return item.possibleStateFlags.solidAllowed;
            case ItemType.dust:
                return item.possibleStateFlags.dustAllowed;  
            default:
                Debug.LogWarning("Is this item empty?",item);
                return false;
        }
    }
}

[System.Serializable]
public class ItemFlags{
    public bool dustAllowed;
    public bool gasAllowed;
    public bool liquidAllowed;
    public bool solidAllowed;
}

