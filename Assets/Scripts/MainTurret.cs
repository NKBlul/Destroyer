using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTurret : MonoBehaviour
{
    [SerializeField] public Transform turretAmmoSpawn;
    [SerializeField] private GameObject turretAmmoPrefab;
    private float speed = 1f;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GetIsActive() == true) 
        {
            print("fire");
            Fire();
        }
    }

    private void Fire()
    {
        GameObject shot = PhotonNetwork.Instantiate(turretAmmoPrefab.name, turretAmmoSpawn.position,
            turretAmmoSpawn.transform.rotation);

        Rigidbody rb = shot.transform.GetComponent<Rigidbody>();
        rb.AddForce(turretAmmoSpawn.forward * speed, ForceMode.Impulse);
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
