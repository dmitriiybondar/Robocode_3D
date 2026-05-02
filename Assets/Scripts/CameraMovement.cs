using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 carPos = new Vector3(
            target.position.x  + _offset.x,
            _offset.y,
            transform.position.z + _offset.z);
        
        transform.position = Vector3.Lerp(
            transform.position, 
            carPos, speed * Time.deltaTime);
    }
}
