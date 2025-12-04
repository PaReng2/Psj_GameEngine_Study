using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCraftingItem : MonoBehaviour
{
    public Button CraftingItem1;
    public Button CraftingItem2;
    public Button CraftingItem3;

    CraftingPanel _CraftingPanel;

    private void Awake()
    {
        _CraftingPanel = FindAnyObjectByType<CraftingPanel>();
    }
    public void axe()
    {
        _CraftingPanel.DoCraft();
    }


}
