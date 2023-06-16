using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseCharacter
{
    Rigidbody rb;
    PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
        {
            Destroy(rb);
        }
    }

    private void Update()
    { 
        if (!pv.IsMine)
        {
            return;
        }
    }

   
}
