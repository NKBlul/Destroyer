using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour
{
    [SerializeField] public Transform turretAmmoSpawn;
    [SerializeField] private GameObject turretAmmoPrefab;
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
        if (Input.GetKeyDown(KeyCode.Space) && GetIsActive() == true && ChangeCamera.Instance.turretCam == true) 
        {
            Fire();
        }
        if (Input.GetKey(KeyCode.A) && GetIsActive() == true && ChangeCamera.Instance.turretCam == true)
        {
            TurnLeft();
        }
        if (Input.GetKey(KeyCode.D) && GetIsActive() == true && ChangeCamera.Instance.turretCam == true)
        {
            TurnRight();
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
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.Self);
        float clampRotation = Mathf.Clamp(transform.eulerAngles.z, -180, -90);
        if (transform.rotation.z == -180.0f)
        {
            rotateSpeed = 0;
        }
    }    

    private void TurnRight() 
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime, Space.Self);
        float clampRotation = Mathf.Clamp(transform.eulerAngles.z, -90, 0);
        if (transform.rotation.z == 0f)
        {
            rotateSpeed = 0;
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
}
