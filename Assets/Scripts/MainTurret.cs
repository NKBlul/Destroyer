using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour
{
    [SerializeField] public Transform turretAmmoSpawn;
    [SerializeField] private GameObject turretAmmoPrefab;
    [SerializeField] private Transform turret;
    private float speed = 100f;
    //private Vector3 rotateSpeed = new Vector3(0, 30, 0);
    private float rotateSpeed = 30f;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        //SetNotActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetIsActive() == true && ChangeCamera.Instance.turretCam == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
            if (Input.GetKey(KeyCode.A))
            {
                TurnLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                TurnRight();
            }
            if (Input.GetKey(KeyCode.W))
            {
                Up();
            }
            if (Input.GetKey(KeyCode.S))
            {
                Down();
            }
        }

        
    }

    private void Fire()
    {
        GameObject shot = PhotonNetwork.Instantiate(turretAmmoPrefab.name, turretAmmoSpawn.position,
            turretAmmoSpawn.transform.rotation);

        Rigidbody rb = shot.GetComponent<Rigidbody>();
        rb.AddForce(turretAmmoSpawn.forward * speed, ForceMode.Impulse);
    }

    void TurnLeft()
    {
        float clampRotation = Mathf.Clamp(transform.eulerAngles.z, -180, -90);
        
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.Self);
    }    

    private void TurnRight() 
    {
        float clampRotation = Mathf.Clamp(transform.eulerAngles.y, -90, 0);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void Up()
    {
        turret.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void Down()
    {
        turret.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);
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
}
