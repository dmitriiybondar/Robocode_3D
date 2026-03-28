using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float gravity = 9.8f, speed = 5f, jumpForce = 8f, turnSpeed = 90f;
    private float _verticalSpeed = 0f, _mouseX = 0f, _mouseY = 0f, _currentAngleX = 0f;

    private CharacterController _controller;
    [SerializeField] private Camera _camera;
    
    [SerializeField] GameObject particleObject, tool;
    private const float hitScaleSpeed = 15f;
    private float hitLastTime = 0f;

    private InventoryManager inventoryManager;
    public List<ItemData> inventoryItems, currentChestItems;
    private Transform itemParent;
    public bool canMove = true;

    private void Dig(Block block)
    {
        if (Time.time - hitLastTime > 1 / hitScaleSpeed)
        {
            tool.GetComponent<Animator>().SetTrigger("attack");
            hitLastTime = Time.time;
            block.health -= tool.GetComponent<Tool>().damageToBlock;
            GameObject go = Instantiate(particleObject, 
                                        block.gameObject.transform.position, 
                                        Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material =
                block.gameObject.GetComponent<MeshRenderer>().material;
            if (block.health <= 0)
            {
                block.DestroyBehaviour();
            }
        }
    }
    
    private void ObjectInteraction(GameObject tempObject)
    {
        switch (tempObject.tag)
        {
            case "Block":
                Dig(tempObject.GetComponent<Block>());
                    break;
            case "Enemy":
                break;
            case "Chest":
                currentChestItems = tempObject.GetComponent<Chest>().chestItems;
                OpenChest();
                break;
        }
    }
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        itemParent = GameObject.Find("InventoryContent").transform;
        inventoryManager.CreateItem(0, inventoryItems);
        canMove = true;
    }

    private void RotateCharacter()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(new Vector3(0f, _mouseX * turnSpeed * Time.deltaTime, 0f));
        _currentAngleX += _mouseY * turnSpeed * Time.deltaTime * -1f;
        _currentAngleX = Mathf.Clamp(_currentAngleX, -90f, 90f);
        
        _camera.transform.localEulerAngles = new Vector3(_currentAngleX, 0f, 0f);
    }

    private void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * speed;
        
        if (_controller.isGrounded)
        {
            _verticalSpeed = 0f;
            if (Input.GetButton("Jump"))
            {
                _verticalSpeed = jumpForce;
            }
        }
        
        _verticalSpeed -= gravity * Time.deltaTime;
        velocity.y = _verticalSpeed;
        _controller.Move(velocity * Time.deltaTime);
    }
    
    

    void Update()
    {
        if (canMove)
        {
            RotateCharacter();
            MoveCharacter();
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 5f))
            {
                if (Input.GetMouseButton(0))
                {
                    ObjectInteraction(hit.transform.gameObject);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E) &&
            !inventoryManager.inventoryPanel.activeSelf)
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            CloseInventoryPanels();
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.StartsWith("mini"))
        {
            inventoryManager.CreateItem(2, inventoryItems);
            Destroy(col.gameObject);
        }
    }
    void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = false;

        inventoryManager.inventoryPanel.SetActive(true);
        if (inventoryItems.Count > 0)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                inventoryManager.InstantiatingItem(inventoryItems[i], itemParent, inventoryManager.inventorySlots);
            }
        }
    }
    void OpenChest()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = false;
        if (!inventoryManager.chestPanel.activeSelf)
        {
            inventoryManager.chestPanel.SetActive(true);
            Transform itemParent = GameObject.Find("ChestContent").transform;
            for (int i = 0; i < currentChestItems.Count; i++)
            {
                inventoryManager.InstantiatingItem(currentChestItems[i], itemParent, inventoryManager.currentChestSlots);
            }
        }
    }
    void CloseInventoryPanels()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = true;
        foreach (GameObject slot in inventoryManager.currentChestSlots)
        {
            Destroy(slot);
        }
        foreach (GameObject slot in inventoryManager.inventorySlots)
        {
            Destroy(slot);
        }
        inventoryManager.currentChestSlots.Clear();
        inventoryManager.inventorySlots.Clear();
        inventoryManager.inventoryPanel.SetActive(false);
        inventoryManager.chestPanel.SetActive(false);
    }
    
}
