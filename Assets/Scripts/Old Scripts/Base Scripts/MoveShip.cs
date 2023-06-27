using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.forward;
    [SerializeField] float shipSpeed = 5;
    [SerializeField] float turnSpeed = 60f;

    Transform myT;

    void Awake()
    {
        myT = transform;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //commented out for now because its causing the player to moved cuz its dragged by the flooring 
        transform.Translate(direction * shipSpeed * Time.deltaTime);
        Turn();
    }

    //makes the ship turn using the J and L keys
    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Turning");
        myT.Rotate(0, yaw, 0);
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
