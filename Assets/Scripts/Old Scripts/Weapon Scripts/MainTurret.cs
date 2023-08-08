using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainTurret : MonoBehaviour
{
    [SerializeField] public Transform turretAmmoSpawn;
    [SerializeField] private GameObject turretAmmoPrefab;
    [SerializeField] private Transform turret;
    private float speed = 100f;
    //private Vector3 rotateSpeed = new Vector3(0, 30, 0);
    private float rotateSpeed = 30f;

    public bool isActive;

    private float minZRot = 180;
    private float maxZRot = 359;
    private float minYRot = 1f;
    private float maxYRot = 25;
    private Vector3 currentRotation;
    private Vector3 turretCurrentRotation;

    PlayerControls _controllerInput;

    void Awake()
    {
        _controllerInput = new PlayerControls();

        _controllerInput.Turret.Shoot.performed += ctx => Fire();

        //_controllerInput.Turret.TurnLeft.performed += ctx => TurnLeft();
        //_controllerInput.Turret.TurnRight.performed += ctx => TurnRight();
        //_controllerInput.Turret.LookUp.performed += ctx => Up();
        //_controllerInput.Turret.LookDown.performed += ctx => Down();
    }

    void Update()
    {
        if (GetIsActive() == true && ChangeCamera.Instance.turretCam == true)
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Fire();
            //}
            if (_controllerInput.Turret.TurnLeft.IsPressed())
            {
                TurnLeft();
            }
            if (_controllerInput.Turret.TurnRight.IsPressed())
            {
                TurnRight();
            }
            if (_controllerInput.Turret.LookUp.IsPressed())
            {
                Up();
            }
            if (_controllerInput.Turret.LookDown.IsPressed())
            {
                Down();
            }
        }       
    }

    private void Fire()
    {
        if (GetIsActive() == true && ChangeCamera.Instance.turretCam == true)
        {
            GameObject shot = PhotonNetwork.Instantiate(turretAmmoPrefab.name, turretAmmoSpawn.position,
            turretAmmoSpawn.transform.rotation);

            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.AddForce(turretAmmoSpawn.forward * speed, ForceMode.Impulse);
        }
    }

    void TurnLeft()
    {
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.Self);
            if (transform.localEulerAngles.y <= minZRot)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, minZRot, transform.localEulerAngles.z);
            }
    }

    private void TurnRight()
    {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.Self);
            if (transform.localEulerAngles.y >= maxZRot)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, maxZRot, transform.localEulerAngles.z);
            }
    }

    private void Up()
    {
            turret.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
            if (turret.localEulerAngles.y >= maxYRot)
            {
                turret.localEulerAngles = new Vector3(0, maxYRot, 0);
            }
    }

    private void Down()
    {
            turret.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);
            if (turret.localEulerAngles.y <= minYRot)
            {
                turret.localEulerAngles = new Vector3(0, minYRot, 0);
            }
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetActive()
    {
        isActive = true;
    }

    public void SetNotActive()
    {
        isActive = false;
    }

    public Camera GetCamera()
    {
        return Camera.main;
    }

    private void OnEnable()
    {
        _controllerInput.Turret.Enable();
    }

    private void OnDisable()
    {
        _controllerInput.Turret.Disable();
    }

}