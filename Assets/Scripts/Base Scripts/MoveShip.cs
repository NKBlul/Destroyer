using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.forward;
    [SerializeField] float shipSpeed = 5;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //commented out for now because its causing the player to moved cuz its dragged by the flooring 
        transform.Translate(direction * shipSpeed * Time.deltaTime);
        //print(transform.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
