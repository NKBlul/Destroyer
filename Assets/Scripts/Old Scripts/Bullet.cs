using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    Rigidbody rb;

    PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        Invoke("DestroyBullet", life);
        Destroy(gameObject, life);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyControllerAI.instance.TakeDamage(10);
        }

        Destroy(gameObject);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        if (pv != null && (pv.IsMine || PhotonNetwork.IsMasterClient))
        {
            // Transfer ownership to master client if not already owned by it
            if (!pv.IsMine)
            {
               // pv.TransferOwnership(PhotonNetwork.MasterClient);
                PhotonNetwork.Destroy(gameObject);
            }
        }
       
    }
}
