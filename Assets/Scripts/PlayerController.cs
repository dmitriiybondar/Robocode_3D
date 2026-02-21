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
        }
    }
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
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
