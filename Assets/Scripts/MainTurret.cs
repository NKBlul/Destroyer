using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour
{
    [SerializeField] public Transform turretAmmoSpawn;
    [SerializeField] private GameObject turretAmmoPrefab;
    private float speed = 100f;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        SetNotActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GetIsActive() == true) 
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.A) && GetIsActive() == true)
        {
            TurnLeft();
        }
        if (Input.GetKeyDown(KeyCode.D) && GetIsActive() == true)
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
        
    }    

    private void TurnRight() 
    {
        print(6);
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
