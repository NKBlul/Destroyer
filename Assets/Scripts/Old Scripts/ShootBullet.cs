using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] public Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletSpeed = 5f;

    PhotonView pv;

    // For controller input system
    PlayerControls _controllerInput;

    void Awake()
    {
       pv = GetComponent<PhotonView>();

       _controllerInput = new PlayerControls();

       _controllerInput.Default.Shoot.performed += ctx => Shooting();
    }

    private void Start()
    {
        if (!pv.IsMine && pv != null)
        {
            //Destroy(pv);
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    Shooting();
        //}
    }

    private void Shooting()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.position,
            bulletSpawn.transform.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        _controllerInput.Default.Enable();
    }

    private void OnDisable()
    {
        _controllerInput.Default.Disable();
    }
}
