using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour
{
    public Inventory Inventory;
    public List<CraftingRecipe> recipeList;
    public GameObject root;
    public TMP_Text plannedText;
    public Button craftButton;
    public Button clearButton;
    public TMP_Text hintText;

    readonly Dictionary<ItemType, int> planned = new();

    bool isOpen;

    void Start()
    {
        SetOpen(false);
        craftButton.onClick.AddListener(DoCraft);
        clearButton.onClick.AddListener(ClearPlanned);
        RefreshPlannedUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetOpen(!isOpen);
        }
    }

    public void SetOpen(bool open)
    {
        isOpen = open;
        if (root)
        {
            root.SetActive(open);
        }
        if (!open)
        {
            ClearPlanned();
        }
    }
    
    public void AddPlanned(ItemType type, int count = 1)
    {
        if (!planned.ContainsKey(type))
            planned[type] = 0;
        planned[type] += count;
        RefreshPlannedUI();
        SetHint($"{type} *{count} Add Complete");
    }
    public void ClearPlanned()
    {
        planned.Clear();
        RefreshPlannedUI();
        SetHint("Cleared");
    }
    void RefreshPlannedUI()
    {
        if (!plannedText) return;

        if (planned.Count == 0)
        {
            plannedText.text = "Add Material to right Click";
            return;
        }

        var sb = new StringBuilder();

        foreach (var item in planned)
            sb.AppendLine($"{item.Key} *{item.Value}");
        plannedText.text = sb.ToString();
    }

    void SetHint(string msg)
    {
        if(hintText)
            hintText.text = msg;
    }

    public void DoCraft()
    {
        if (planned.Count == 0)
        {
            SetHint("need more Material");
            return;
        }

        foreach (var plannedItem in planned)
        {
            if (Inventory.GetCount(plannedItem.Key) < plannedItem.Value)
            {
                SetHint($"need more {plannedItem.Key} ");
                return;
            }
        }

        var matchedProduct = FindMatch(planned);
        if (matchedProduct == null)
        {
            SetHint("no exist recipe");
            return;
        }

        foreach (var itemforConsume in planned)
            Inventory.Consume(itemforConsume.Key, itemforConsume.Value);

        foreach (var p in matchedProduct.outputs)
            Inventory.Add(p.type, p.count);

        ClearPlanned();

        SetHint($"crafted : {matchedProduct.displayName}");
    }

    CraftingRecipe FindMatch(Dictionary<ItemType, int> planned)
    {
        foreach (var recipe in recipeList)
        {
            bool ok = true;
            foreach (var ing in recipe.inputs)
            {
                if (!planned.TryGetValue(ing.type, out int have) || have != ing.count)
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
                return recipe;
        }
        return null;
    }
}
