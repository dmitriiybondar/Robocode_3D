using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    public GameObject inventoryPanel, chestPanel, descriptionPanel;
    public GameObject invContent, chestContent;
    public ItemData[] items;
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> currentChestSlots = new List<GameObject>();

    private void Awake()
    {
        /*inventoryPanel = GameObject.Find("TotalInventoryPanel");
        chestPanel = GameObject.Find("ChestPanel");
        invContent = GameObject.Find("InventoryContent");
        chestContent = GameObject.Find("ChestContent");
        descriptionPanel = GameObject.Find("DescriptionPanel");*/
    }
    private void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
        descriptionPanel.SetActive(false);
    }
    public void CreateItem(int itemId, List<ItemData> itemsList)
    {
        ItemData item = new ItemData(items[itemId].name, items[itemId].id,
            items[itemId].count, items[itemId].isUniq, items[itemId].description);

        if(!item.isUniq && item.count > 0)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (item.id == itemsList[i].id)
                {
                    itemsList[i].count++;
                    break;
                }
                else if (i == itemsList.Count - 1)
                {
                    itemsList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && itemsList.Count == 0))
        {
            itemsList.Add(item);
        }
    }
   public void InstantiatingItem(ItemData itemData, Transform parent,List<GameObject> itemList)
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
