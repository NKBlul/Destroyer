using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAmmo : MonoBehaviour
{
    public float life = 5;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyControllerAI.instance.TakeDamage(50);
        }
        Destroy(gameObject);
        PhotonNetwork.Destroy(gameObject);
    }
}
