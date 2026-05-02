using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] Transform centerMass;
    [SerializeField] private float motorTorque;
    [SerializeField] private float maxSteer;
    private float _horizontal;
    private float _vertical;
    private Rigidbody _rb;
    [SerializeField] private WheelScript[] wheels;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerMass.localPosition;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical") * -1;
        foreach (WheelScript wheel in wheels)
        {
            wheel.ChangeSteerAngle(_horizontal *  motorTorque);
            wheel.ChangeMotorTorque(_vertical *  motorTorque);
        }
    }
}
