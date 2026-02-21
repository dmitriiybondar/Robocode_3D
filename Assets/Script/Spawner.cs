using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject bedHeartPrefab;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while(true)
        {
            if(Random.Range(0,100) > 25)
            {
                Destroy(Instantiate(heartPrefab, transform.position, Quaternion.identity), 5f);
            }
            else
            {
                Destroy(Instantiate(bedHeartPrefab, transform.position, Quaternion.identity),5f);
            }
            
            yield return new WaitForSeconds(timer);
        }
    }
}
