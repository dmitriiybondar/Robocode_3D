using System;
using UnityEngine;
public class Puhkalka : MonoBehaviour
{
    GameObject hitObject;
    [SerializeField] GameObject cubePrefab;

   int size = 11;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                CreatePyramid(size * i,  size * j);
            }
        }
    }
    private void CreatePyramid(int offsetX, int  offsetZ)
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = y + offsetX; x < size - y + offsetX; x++)
            {
                for (int z = y + offsetZ; z < size - y + offsetZ; z++)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    Instantiate(cubePrefab,  pos, Quaternion.identity);
                }
            }
        }
    }

    private void Update()
    {
       RaycastHit hit;
        Ray cameraRay =  Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out hit))
        {
            if (hitObject && hitObject != hit.transform.gameObject)
            {
                hitObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            hitObject = hit.transform.gameObject;
            hitObject.GetComponent<MeshRenderer>().material.color = Color.red;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newCube = Instantiate(cubePrefab);
                Vector3 pos = hitObject.transform.position;
                pos.y += 1;
                newCube.transform.position = pos;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(hitObject);
            }
        }
        else if (hitObject)
        {
            hitObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
