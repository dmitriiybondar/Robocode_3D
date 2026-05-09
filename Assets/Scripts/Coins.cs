using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject coinFx;
    private MeshRenderer _mesh;
    private BoxCollider _coinCollider;
    private Rigidbody _rb;

    private void Awake()
    {
        _coinCollider = GetComponent<BoxCollider>();
        _mesh = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.angularVelocity = new Vector3(0f, rotateSpeed, 0f);
    }

    public int GetPrice()
    {
        return price;
    }

    public void PickUp()
    {
        _mesh.enabled = false;
        _coinCollider.enabled = false;
        int randomTime = Random.Range(5, 8);
        Instantiate(coinFx, transform.position, Quaternion.identity);
        Invoke("Respawn", randomTime);
    }

    public void Respawn()
    {
        _mesh.enabled = true;
        _coinCollider.enabled = true;
    }
}
