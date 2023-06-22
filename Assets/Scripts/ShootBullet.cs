using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject myBullet;
    public float speed = 100; 


    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject myBulletPrefab = Instantiate(myBullet, 
                transform.position, Quaternion.identity) as GameObject; 

            Rigidbody myBulletPrefabRigidbody = myBulletPrefab.GetComponent<Rigidbody>();
            myBulletPrefabRigidbody.AddForce(Vector3.forward * speed);
        }
    }
}
