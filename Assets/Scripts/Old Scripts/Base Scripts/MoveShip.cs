using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveShip : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.forward;
    [SerializeField] float shipSpeed = 5;
    [SerializeField] float turnSpeed = 60f;

    [SerializeField] float shipIntervals = 5f;

   // public float acceleration = 100f;
    //public float deceleration = 10f;
    //public float brake = 70f;

    Transform myT;
    private bool isOnPlatform = false;

    private float maxSpeed;
    private float minSpeed;

    PlayerControls _controllerInput;
    Vector2 _movement;

    void Awake()
    {
        myT = transform;
        maxSpeed = 90f;
        minSpeed = 0f;

        _controllerInput = new PlayerControls();
        _controllerInput.Ship.Move.performed += ctx => _movement = ctx.ReadValue<Vector2>();
        _controllerInput.Ship.Move.canceled += ctx => _movement = Vector2.zero;
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
        if (!ChangeCamera.Instance.shipCam)
        {
            OnDisable();
        }
        else
        {
            OnEnable();
        }
        if (_controllerInput.Ship.Acceleration.IsPressed())
        {
            shipSpeed += 3.0f * Time.deltaTime;
        }
        if (_controllerInput.Ship.Deceleration.IsPressed())
        {
            shipSpeed -= 3.0f * Time.deltaTime;
        }


        shipSpeed = Mathf.Clamp(shipSpeed, 0f, maxSpeed);
    }

    public void IncreaseShipSpeed()
    {
        if (shipSpeed >= maxSpeed)
        {
            shipSpeed = maxSpeed;
        }

        shipSpeed += shipIntervals;
        //print(shipSpeed);
    }
    public void DecreaseShipSpeed()
    {
        if (shipSpeed <= minSpeed)
        {
            shipSpeed = minSpeed;
        }
        shipSpeed -= shipIntervals;
        //print(shipSpeed);
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
        float yaw = turnSpeed * Time.deltaTime * _movement.x;
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

    private void OnEnable()
    {
        _controllerInput.Ship.Enable();
    }

    private void OnDisable()
    {
        _controllerInput.Ship.Disable();
    }
}
