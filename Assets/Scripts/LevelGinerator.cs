using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class LevelGinerator : MonoBehaviour
{
    [SerializeField] private GameObject groundPref, grassPref;

    private int _baseHeight = 1,
                _maxBlockCountY = 10,
                _chunkSize = 16,
                _perLinNoiseSensetivity = 25,
                _chunkCount = 4;

    private float seedX, seedY;

    private void CreateChunk(int chunkNumX, int chunkNumZ)
    {
        GameObject chunk = new GameObject();
        float chunkX = chunkNumX * _chunkSize + _chunkSize / 2;
        float chunkZ = chunkNumZ * _chunkSize + _chunkSize / 2;
        chunk.transform.position = new Vector3(chunkX, 0, chunkZ);
        chunk.name = "Chunk_" + chunkNumX + ", " + chunkNumZ;
        chunk.AddComponent <MeshFilter>();
        chunk.AddComponent<MeshRenderer>();
        chunk.AddComponent<Chunk>();

        for (int x = chunkNumX * _chunkSize; x < chunkNumX + _chunkSize; x++)
        {
            for (int z = chunkNumZ *_chunkSize; z < chunkNumZ * _chunkSize + _chunkSize; z++)
            {
                float xSample = seedX + (float)x / _perLinNoiseSensetivity;
                float ySample = seedY + (float)z / _perLinNoiseSensetivity;
                float sample = Mathf.PerlinNoise(xSample, ySample);
                int height = _baseHeight * (int)(sample * _maxBlockCountY);

                for (int y = 0; y < height; y++)
                {
                    GameObject block;
                    if (y == height - 1)
                    {
                        block = Instantiate(grassPref, new Vector3(x, y, z), Quaternion.identity);
                    }
                    else
                    {
                        block = Instantiate(groundPref, new Vector3(x, y, z), Quaternion.identity);
                    }
                    block.transform.SetParent(chunk.transform);
                }
            }
        }
    }
    
    void Start()
    {
        seedX = Random.Range(0, 10);
        seedY = Random.Range(0, 10);
        for (int x = 0; x < _chunkCount; x++)
        {
            for (int z = 0; z < _chunkSize; z++)
            {
                CreateChunk(x, z);
            }
        }
        
    }


    void Update()
    {
        
    }
}
