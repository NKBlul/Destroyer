using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] public Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletSpeed = 3f;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        GameObject cb = Instantiate(bulletPrefab, bulletSpawn.position,
            bulletSpawn.transform.rotation);

        Rigidbody rig = cb.GetComponent<Rigidbody>();
        rig.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
    }
}
