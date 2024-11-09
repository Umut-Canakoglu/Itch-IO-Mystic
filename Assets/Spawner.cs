using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] items;
    private float count = 0;
    void Start()
    {
        float parts = 18/items.Length;
        foreach (GameObject item in items) 
        {
            float xPosition = Random.Range((count*parts)-9f, ((count+1)*parts)-9f);
            float yPosition = Random.Range(-5f, 5f);
            Vector2 positionObject = new Vector2(xPosition, yPosition);
            Instantiate(item, positionObject, Quaternion.identity);
            count ++;
        }
    }
}
