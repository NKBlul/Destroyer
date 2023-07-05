using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviourPunCallbacks
{
    [SerializeField] public float m_Health = 100.0f;
    [SerializeField] public float m_DamageDealt = 20.0f;
    [SerializeField] public float m_Speed = 5.0f;
    //[SerializeField] string m_CharacterRole = "Null";

    public Rigidbody rb;
    public PhotonView pv;

    protected Rigidbody m_Rigidbody;

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
            Destroy(pv);
        }
    }

    private void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }
    }

    public void Attack()
    {
        Debug.Log("Damage: " + m_DamageDealt);
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        if (m_Health <= 0)
        {
            Die();
            //pv.RPC("Die", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void Die()
    {
        Destroy(gameObject);
        //PhotonNetwork.Destroy(gameObject);
        //PhotonNetwork.Destroy(pv);
        if (pv != null && (pv.IsMine || PhotonNetwork.IsMasterClient))
        {
            // Transfer ownership to master client if not already owned by it
            if (!pv.IsMine)
            {
                pv.TransferOwnership(PhotonNetwork.MasterClient);
                PhotonNetwork.Destroy(gameObject);
            }
        }
        Debug.Log(gameObject.name + " ded");
    }
}
