using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.forward;
    [SerializeField] float shipSpeed = 5;
    [SerializeField] float turnSpeed = 60f;

   // public float acceleration = 100f;
    //public float deceleration = 10f;
    //public float brake = 70f;

    Transform myT;
    private bool isOnPlatform = false;

    private float maxSpeed;

    void Awake()
    {
        myT = transform;
        maxSpeed = 100f;

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

        if (Input.GetKey(KeyCode.I))
        {
            float acceleration = 3f;
            shipSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            float deceleration = 3f;
            shipSpeed -= deceleration * Time.deltaTime;
        }

        
        shipSpeed = Mathf.Clamp(shipSpeed, 3f, maxSpeed);
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetKey(KeyCode.I))
    //    {
    //        float acceleration = 50f;
    //        shipSpeed += acceleration * Time.deltaTime;
    //    }
    //    else
    //    {
    //        float deceleration = 10f;
    //        shipSpeed -= deceleration * Time.deltaTime;
    //    }

    //    if (Input.GetKey(KeyCode.K))
    //    {
    //        float brake = 30f;
    //        shipSpeed -= brake * Time.deltaTime;
    //    }
    //    shipSpeed = Mathf.Clamp(shipSpeed, 0f, maxSpeed);

    //}

    //makes the ship turn using the J and L keys
    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Turning");
        myT.Rotate(0, yaw, 0);
    }

    //private void Acceleration()
    //{
    //    if (Input.GetKey(KeyCode.I))
    //    {
    //        float acceleration = 20f;
    //        shipSpeed += acceleration * Time.deltaTime;
    //    }
    //    else
    //    {
    //        float deceleration = 10f;
    //        shipSpeed -= deceleration * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.K))
    //    {
    //        float brakeSpeed = 30f;
    //        shipSpeed -= brakeSpeed * Time.deltaTime;

    //    }

    //    shipSpeed = Mathf.Clamp(shipSpeed, 0f, maxSpeed);
    //}
}
