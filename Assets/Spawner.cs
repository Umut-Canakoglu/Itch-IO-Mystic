using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] items;
    void Start()
    {
       foreach (GameObject item in items) 
       {
            float xPosition = Random.Range(-9f, 9f);
            float yPosition = Random.Range(-5f, 5f);
            Vector2 positionObject = new Vector2(xPosition, yPosition);
            Instantiate(item, positionObject, Quaternion.identity);
       }
    }
}
