using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    public GameObject inventoryPanel, chestPanel;
    public GameObject invContent, chestContent;
    public ItemData[] items;
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> currentChestSlots = new List<GameObject>();

    private void Awake()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");
        chestPanel = GameObject.Find("ChestPanel");
        invContent = GameObject.Find("InventoryContent");
        chestContent = GameObject.Find("ChestContent");
    }
    
    private void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
    }

    public void CreateItem(int itemId, List<ItemData> itemList)
    {
        ItemData item = new ItemData(items[itemId].name, items[itemId].id,
            items[itemId].count, items[itemId].isUniq, items[itemId].description);

        if (!item.isUniq && item.count > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (item.id == itemList[i].id)
                {
                    itemList[i].count++;
                    break;
                }
                else if (i == itemList.Count - 1)
                {
                    itemList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && item.count == 0))
        {
            itemList.Add(item);
        }
    }

    public void InstantiatingItem(ItemData itemData, Transform parent, List<GameObject> itemList)
    {
        GameObject go = Instantiate(slotPrefab);
        go.transform.SetParent(parent.transform);
        go.AddComponent<Slot>();
        go.GetComponent<Slot>().itemData = itemData;
        go.transform.Find("Name").GetComponent<Text>().text = itemData.name;
        go.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(itemData.name);
        go.transform.Find("Value").GetComponent<Text>().text = itemData.count.ToString();
        go.transform.Find("Value").GetComponent<Text>().color = itemData.isUniq ? Color.clear : Color.white;
        itemList.Add(go);
    }
}
