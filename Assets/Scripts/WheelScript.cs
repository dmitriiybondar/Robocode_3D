using UnityEngine;

public class WheelScript : MonoBehaviour
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private bool isSteer;
    [SerializeField] private bool isInvertSteer;
    [SerializeField] private bool isPower;
    private float _streetAngle;
    private float _motorTorque;
    private WheelCollider _wheelCollider;

    void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
    }

    void Update()
    {
        _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation * Quaternion.Euler(0, -90f, 0);
    }

    private void FixedUpdate()
    {
        if (isSteer)
        {
            _wheelCollider.steerAngle = _streetAngle * (isInvertSteer ? -1 : 1);
        }

        if (isPower)
        {
            _wheelCollider.motorTorque = _motorTorque;
        }
    }

    public void ChangeMotorTorque(float torque)
    {
        _motorTorque = torque;
    }

    public void ChangeSteerAngle(float angle)
    {
        _streetAngle = angle;
    }
}
