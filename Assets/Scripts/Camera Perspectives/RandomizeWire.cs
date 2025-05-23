using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeWire : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int newSpot = Random.Range(0, transform.childCount);
            Vector3 tempPosition = transform.GetChild(i).position;
            transform.GetChild(i).position = transform.GetChild(newSpot).position;
            transform.GetChild(newSpot).position = tempPosition;
        }
    }
}
