using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hernan;

public class ItemStats : MonoBehaviour
{
    public enum ItemState
    {
        OnFloor,
        PickedUp
    }

    public string itemName = "New Item";
    public ItemState itemState = ItemState.OnFloor;
    public List<GameObject> buffEffects = new List<GameObject>(); // All buffs must be created so that if destroyed, Changed properties must return.
    public GameObject itemOwner = null; // The object currently holding this item.
    public int level = 1;

    public bool buffsEnabled = false;
    public bool isEquipable = false; //if object is not equipable and is Pasive than instantiate buffs // if isequpable and its not equpited buffs will not be instantiated
    public bool equipped = false;
    public bool broken = false;

    public float currentDurability = 100f;
    public float durability = 100f;

    public bool passive = false; // if true than object doesnt need to be pressed to use. if false than you must press the item in order to get the effects.
    public bool oneTimeUse = false; // if true than object will destroy itself after use. if false than object will reset.

    DefenceStats Stats = new DefenceStats();

    void Update()
    {
        if ((itemState == ItemState.PickedUp) && (currentDurability >= 0 && !broken))
        {
            // if its armour
            if (isEquipable && equipped)
            {
                if (passive && !oneTimeUse)
                {
                    // armour with aura
                    InstantiateBuffs();
                }
            }
            // Passive Item
            if ((!isEquipable && passive) && (!oneTimeUse))
            {
                InstantiateBuffs();
            }
        }
    }
   
    public void InstantiateBuffs()
    {
        if (!buffsEnabled)
        {
            for (int i = 0; i < buffEffects.Count; i++)
            {
                Instantiate(buffEffects[i], itemOwner.transform.position, itemOwner.transform.rotation);
            }
            buffsEnabled = true;
        } 
    }
    void Equipped()
    {
        Stats.EquipUnEquip(Stats, true);
    }

    void UnEquipped()
    {
        if (equipped == true)
        {
            Stats.EquipUnEquip(Stats, false); // Takes off the resistance stats, and defence stats.
        }
    }

    void DestroyOrBreakItem()
    {
        Stats.EquipUnEquip(Stats, false);
    }

    void OnDrawGizmos()
    {
        GameObject me = gameObject;
        if (me.name != itemName)
        {
            me.name = itemName;
            Object myPrefab = UnityEditor.PrefabUtility.GetPrefabObject(me);

            myPrefab.name = me.name;
        }
    }
}
