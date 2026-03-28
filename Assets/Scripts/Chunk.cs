using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private const int VisibilityDistance = 30;
    private bool _isVisible;
    private Transform _playerT;
    private Vector3 _chunkPos;
    
    void Start()
    {
        _playerT = GameObject.Find("Capsule").transform;
        _chunkPos = transform.position;
        _isVisible = true;
    }

    void Update()
    {
        float distance = Vector3.Distance(_chunkPos, new Vector3(_playerT.position.x, 
                                                                0f, 
                                                                _playerT.position.z));
        if (distance > VisibilityDistance && _isVisible)
        {
            SetActivity(false);
        } 
        else if (distance < VisibilityDistance && !_isVisible)
        {
            SetActivity(true);
        }
    }

    private void SetActivity(bool isActive)
    {
        int childrenCount = transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
        
        _isVisible = isActive;
    }
}
