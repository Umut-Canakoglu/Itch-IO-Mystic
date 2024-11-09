using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject[] allItems;
    public Collider2D[] rooms;
    public int dayCount = 0;
    void Awake()
    {
        RandomlySpreadRoomByRoom(DetermineItemsOfDay(dayCount));
    }
    void ReShuffleCollider(Collider2D[] cols) 
    {
        for (int i = 0; i < cols.Length; i++ )
        {
            Collider2D col = cols[i];
            int r = UnityEngine.Random.Range(i, cols.Length);
            cols[i] = cols[r];
            cols[r] = col;
        }
    }
    void ReShuffleGameObject(GameObject[] gameObjects) 
    {
        for (int i = 0; i < gameObjects.Length; i++ )
        {
            GameObject objectGame = gameObjects[i];
            int r = UnityEngine.Random.Range(i, gameObjects.Length);
            gameObjects[i] = gameObjects[r];
            gameObjects[r] = objectGame;
        }
    }
    private bool IsFoundGameObject(GameObject[] objects, GameObject thing)
    {
        bool isFound = false;
        foreach (GameObject gameObject in objects)
        {
            if (thing == gameObject)
            {
                isFound = true;
                break;
            }
        }
        return isFound;
    }
    private bool IsFoundInt(int[] nums, int num)
    {
        bool isFound = false;
        foreach (int integer in nums)
        {
            if (num == integer)
            {
                isFound = true;
                break;
            }
        }
        return isFound;
    }
    private void RandomlySpreadRoomByRoom(GameObject[] items)
    {
        int numOf = items.Length;
        int usableItems = numOf;
        int numOfItems;
        ReShuffleCollider(rooms);
        foreach (Collider2D room in rooms)
        {
            if (usableItems == 0)
            {
                break;
            }
            int roomIndex = Array.IndexOf(rooms, room);
            if (roomIndex == rooms.Length-1)
            {
                numOfItems = usableItems;
            } else {
                numOfItems = UnityEngine.Random.Range(0, usableItems);
            }
            if (numOfItems == 0 )
            {
                continue;
            }
            usableItems -= numOfItems;
            Rect[] regions = new Rect[numOfItems];
            GameObject[] itemsUsed = new GameObject[numOfItems];
            float regionWidth = (room.bounds.max.x - room.bounds.min.x) % numOfItems;
            float regionHeight = (room.bounds.max.y - room.bounds.min.y) % numOfItems;
            for (int i = 0; i < numOfItems; i++)
            {
                Rect newRegion = new Rect(0, 0, 0, 0);
                bool isOverlapping = true;
                while (isOverlapping)
                {
                    float xMin = UnityEngine.Random.Range(room.bounds.min.x, room.bounds.max.x - regionWidth);
                    float yMin = UnityEngine.Random.Range(room.bounds.min.y, room.bounds.max.y - regionHeight);
                    newRegion = new Rect(xMin, yMin, regionWidth, regionHeight);
                    isOverlapping = false;
                    foreach (Rect region in regions)
                    {
                        if (newRegion == Rect.zero || newRegion.Overlaps(region))
                        {
                            isOverlapping = true;
                            break;
                        }
                    }
                }
                regions[i] = newRegion;
            }
            int total = 0;
            if (roomIndex == rooms.Length - 1)
            {
                int count = 0;
                foreach (GameObject itemGameObject in items)
                {
                    if (itemGameObject != null)
                    {
                        itemsUsed[count] = itemGameObject;
                        count ++;
                    }
                }
            } else {
                while (total < numOfItems)
                {
                    int randomIndex = UnityEngine.Random.Range(0, numOf);
                    if (IsFoundGameObject(itemsUsed, items[randomIndex]) == false && items[randomIndex] != null)
                    {
                        itemsUsed[total] = items[randomIndex];
                        total ++;
                        items[randomIndex] = null;
                    }
                }
            }
            ReShuffleGameObject(itemsUsed);
            for (int i = 0; i < numOfItems; i++)
            {
                float xPosition = UnityEngine.Random.Range(regions[i].xMin, regions[i].xMax);
                float yPosition = UnityEngine.Random.Range(regions[i].yMin, regions[i].yMax);
                Vector2 positionObject = new Vector2(xPosition, yPosition);
                Instantiate(itemsUsed[i], positionObject, Quaternion.identity);
            }   
        }
    }
    private GameObject[] DetermineItemsOfDay(int theDay)
    {
        theDay ++;
        GameObject[] itemsArray = new GameObject[theDay*2];
        int[] randomNums = new int[theDay*2];
        int count = 0;
        while (count < theDay*2)
        {
            int randomNum = UnityEngine.Random.Range(0, allItems.Length);
            if (IsFoundInt(randomNums, randomNum)==false)
            {
                randomNums[count] = randomNum;
                count ++;
            }
        }
        Debug.Log(randomNums);
        for (int i = 0; i < theDay*2; i++)
        {
            itemsArray[i] = allItems[randomNums[i]];
        }
        return itemsArray;
    }
}
