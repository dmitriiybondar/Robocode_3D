using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, 
                    IDragHandler, 
                    IEndDragHandler,
                    IPointerDownHandler,
                    IPointerUpHandler
{
    public ItemData itemData;
    private Transform _tempParentForSlots;
    private InventoryManager _inventoryManager;
    private PlayerController _playerController;
    private string _parentName;

    private void Start()
    {
        _tempParentForSlots = GameObject.Find("Canvas").transform;
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _playerController = GameObject.Find("Capsule").GetComponent<PlayerController>();
        _parentName = transform.parent.name;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _inventoryManager.descriptionPanel.SetActive(false);
        transform.SetParent(_tempParentForSlots);
        transform.position = eventData.position;
    }
    
    public void OnEndDrag(PointerEventData eventData) { }

    public void OnPointerDown(PointerEventData eventData)
    {
        _inventoryManager.descriptionPanel.SetActive(true);
        _inventoryManager.descriptionPanel.transform.position = transform.position;
        if (itemData != null)
        {
            _inventoryManager.descriptionPanel.transform.Find("DesriptionText").GetComponent<Text>().text = 
                                                                                            itemData.description;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inventoryManager.descriptionPanel.SetActive(false);
    }
}
