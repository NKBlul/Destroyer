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
        Invoke(nameof(DestroyBullet), life);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!pv.IsMine && pv != null)
        {
            Destroy(rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine && pv != null)
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<EnemyControllerAI>(out EnemyControllerAI enemy))
        {
            enemy.TakeDamage(10);
        }

        pv.RPC(nameof(DestroyBullet), pv.Owner);
    }

    [PunRPC]
    private void DestroyBullet()
    {
        Destroy(gameObject);
        if (pv != null && (pv.IsMine || PhotonNetwork.IsMasterClient))
        {
            // Transfer ownership to master client if not already owned by it
            if (!pv.IsMine)
            {
                pv.TransferOwnership(PhotonNetwork.MasterClient);
                PhotonNetwork.Destroy(gameObject);
            }

        }
        //PhotonNetwork.Destroy(gameObject);
    }
}
