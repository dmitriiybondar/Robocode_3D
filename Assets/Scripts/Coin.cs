using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject coinFx;
    private MeshRenderer _mesh;
    private BoxCollider _coinCollider;
    private Rigidbody _rb;
}
