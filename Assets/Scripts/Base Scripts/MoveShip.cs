using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.right;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(direction * 5 * Time.deltaTime);
            print(transform.position);
        
    }
}
