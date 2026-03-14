using System.Collections;
using System.Collections.Generic;
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

    private InventoryManager _inventoryManager;
    public List<ItemData> inventoryItems, currentChestItems;
    private Transform _itemParant;
    private bool canMove = true;

    private void Dig(Block block)
    {
        if (Time.time - hitLastTime > 1 / hitScaleSpeed)
        {
            tool.GetComponent<Animator>().SetTrigger("Attack");
            hitLastTime = Time.time;
            block.health -= tool.GetComponent<Tool>().damageToBlock;
            GameObject go = Instantiate(particleObject, block.gameObject.transform.position, Quaternion.identity);
            go.GetComponent<ParticleSystemRenderer>().material = block.gameObject.GetComponent<MeshRenderer>().material;
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
        }
    }
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
        _inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        _itemParant = GameObject.Find("InventoryContent").transform;
        _inventoryManager.CreateItem(0, inventoryItems);
    }

    private void RotateCharacter()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(new Vector3(0f, _mouseX * turnSpeed * Time.deltaTime, 0f));
        _currentAngleX += _mouseY * turnSpeed * Time.deltaTime * -1f;
        _currentAngleX = Mathf.Clamp(_currentAngleX, -60f, 60f);
        
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
            if (Physics.Raycast(_camera.transform.position,
                    _camera.transform.forward, out hit, 5f))
            {
                if (Input.GetMouseButton(0))
                {
                    ObjectInteraction(hit.transform.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.StartsWith("mini"))
        {
            _inventoryManager.CreateItem(2, inventoryItems);
            Destroy(collider.gameObject);
        }
    }

    private void OpenInventory()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = false;
        
        _inventoryManager.inventoryPanel.SetActive(false);
        if (inventoryItems.Count > 0)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                _inventoryManager.InstantiatingItem(inventoryItems[i], _itemParant, _inventoryManager.inventorySlots);
            }
        }
    }

    private void OpenChest()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        canMove = false;

        if (!_inventoryManager.chestPanel.activeSelf)
        {
            _inventoryManager.chestPanel.SetActive(true);
            Transform itemParent = GameObject.Find("ChestContent").transform;
            for (int i = 0; i < currentChestItems.Count; i++)
            {
                _inventoryManager.InstantiatingItem(currentChestItems[i], itemParent, _inventoryManager.inventorySlots);
            }
        }
    }

    private void CloseInventoryPanel()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true;
        foreach (GameObject slot in _inventoryManager.currentChestSlots) 
        {
            Destroy(slot);
        }
        foreach (GameObject slot in _inventoryManager.inventorySlots) 
        {
            Destroy(slot);
        }
        _inventoryManager.currentChestSlots.Clear();
        _inventoryManager.inventorySlots.Clear();
        _inventoryManager.inventoryPanel.SetActive(false);
        _inventoryManager.chestPanel.SetActive(false);
    }
}
