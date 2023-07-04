using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    //public Rigidbody rb;
    //public PhotonView pv;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        //pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        //if (!pv.IsMine)
        //{
        //    Destroy(rb);
        //}
    }

    private void Update()
    {
        //if (!pv.IsMine)
        //{
        //    return;
        //}
    }
}
