using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] public Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletSpeed = 5f;

    PhotonView pv;

    void Awake()
    {
       pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(pv);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.position,
            bulletSpawn.transform.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
    }
}
