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
        //commented out for now because its causing the player to moved cuz its dragged by the flooring 
            //transform.Translate(direction * 5 * Time.deltaTime);
            //print(transform.position);
        
    }
}
