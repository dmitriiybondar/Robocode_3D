using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text lapText;
    [SerializeField] private int maxLap;
    private int _currentLap;
    private Transform _parentCheckpoint;
    private int _checkpointCount;
    private int _checkpointLayer;
    private int _lastCheckPoint;
    private int _currentCheckPoint;

    private void UpdateUI()
    {
        lapText.text = $"Lap: {_currentLap}/{maxLap}";
    }

    private void Awake()
    {
        _currentLap = 1;
        _parentCheckpoint = GameObject.Find("Checkpoints").transform;
        _checkpointCount = _parentCheckpoint.childCount;
        _checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        UpdateUI();
    }

    private void NextLap()
    {
        _currentCheckPoint = 1;
        _currentLap++;
        if (_currentLap >= maxLap)
        {
            // Поки залишаємо пустим
        }
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _checkpointLayer)
        {
            if (int.Parse(other.gameObject.name) == 0
                && _currentCheckPoint == _checkpointCount)
            {
                NextLap();
            }
            else if (int.Parse(other.gameObject.name) == _currentCheckPoint)
            {
                _currentCheckPoint++;
            }
        }
    }
}