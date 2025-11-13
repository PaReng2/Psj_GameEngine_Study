using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public Sprite dirt;
    public Sprite grass;
    public Sprite gold;
    public List<Transform> Slot = new List<Transform>();
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

    public void UpdateInventory(Inventory myinventory)
    {
        foreach (var slotItems in items)
        {
            Destroy(slotItems);
        }
        items.Clear();

        int idx = 0;
        foreach (var item in myinventory.items)
        {
            var go = Instantiate(SlotItem, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;
            SlotItemPrefab sItem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch (item.Key)
            {
                case BlockType.Dirt:
                    sItem.ItemGetting(dirt, item.Value.ToString());
                    break;
                case BlockType.Grass:
                    sItem.ItemGetting(grass, item.Value.ToString());
                    break;
                case BlockType.Gold:
                    sItem.ItemGetting(gold, item.Value.ToString());
                    break;

            }
            idx++;
        }
    }

    
}
