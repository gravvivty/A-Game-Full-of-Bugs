using System.Collections.Generic;
using UnityEngine;

namespace Project.Inventory
{
    /// <summary>
    /// Represents the data for a single inventory item.
    /// This includes the item's name, unique ID, description, and icon.
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public int itemID;
        [TextArea] public string description;
        public Sprite icon;
        
        [System.Serializable]
        public class Combination
        {
            public int otherItemID;
            public ItemData resultItem;
        }
    
        public List<Combination> possibleCombinations;

        public bool CanCombine(int otherItemID)
        {
            return possibleCombinations.Exists(c => c.otherItemID == otherItemID);
        }

        public ItemData GetCombinationResult(int otherItemID)
        {
            Combination combination = possibleCombinations.Find(c => c.otherItemID == otherItemID);
            return combination?.resultItem;

        }
    }
}