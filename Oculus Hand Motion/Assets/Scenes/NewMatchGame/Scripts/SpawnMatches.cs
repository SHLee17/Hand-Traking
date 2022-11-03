using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMatches : MonoBehaviour
{
    public Blank blankPrefab;
    public Transform blankPosition;
    public float posOffsetX, posOffsetY;
    Vector3 spawnPos;
    public bool isGaro;
    
    public void SpawnBlank(int spawnX, int spawnY)
    {
        spawnPos = Vector3.zero;
        if (isGaro)
        {
            int temp = spawnX;
            spawnX = spawnY;
            spawnY = temp;
        }
        for (int i = 0; i <= spawnX; i++)
        {
            for (int j = 0; j <= spawnY - 1; j++)
            {
                spawnPos = Vector3.right * posOffsetX * i + Vector3.forward * posOffsetY * j;
                Blank temp = Instantiate(blankPrefab);

                temp.transform.parent = blankPosition;
                temp.transform.localPosition = spawnPos;
                temp.transform.localRotation = Quaternion.identity;

            }
            spawnPos += Vector3.back * posOffsetY * spawnY;
        }
        
    }
}
