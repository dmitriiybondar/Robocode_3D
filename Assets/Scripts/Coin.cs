using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private GameObject coinFx;
    private MeshRenderer _mesh;
    private CapsuleCollider _coinCollider;
    private Rigidbody _rb;

    private void Awake()
    {
        _coinCollider = GetComponent<CapsuleCollider>();
        _mesh = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    public int GetPrice()
    {
        return price;
    }

    public void PickUp()
    {
        _mesh.enabled = false;
        _coinCollider.enabled = false;
        int randomTime = UnityEngine.Random.Range(5, 8);
        Instantiate(coinFx, transform.position, transform.rotation);
        Invoke("Respawn", randomTime);
    }

    private void Respawn()
    {
        _mesh.enabled = true;
        _coinCollider.enabled = true;
    }
}
