using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Vector3 rotationSpeed = new Vector3(0f, Random.Range(-5f, 5f), 0f);
        Vector3 dir = new Vector3(Random.Range(-0.5f, 0.5f), 1f,0f);

        rb.AddForce(dir* force, ForceMode.Impulse);
        rb.angularVelocity = rotationSpeed;
    }
}
