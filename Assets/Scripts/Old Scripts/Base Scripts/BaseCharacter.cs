using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviourPunCallbacks
{
    [SerializeField] public float m_Health = 60.0f;
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
        if (!pv.IsMine && pv != null)
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
            pv.RPC(nameof(Die), pv.Owner);
        }
    }

    [PunRPC]
    protected virtual void Die()
    {
        Destroy(gameObject);
        //PhotonNetwork.Destroy(gameObject);
        if (pv != null && (pv.IsMine || PhotonNetwork.IsMasterClient))
        {
            // Transfer ownership to master client if not already owned by it
            if (!pv.IsMine)
            {
                pv.TransferOwnership(PhotonNetwork.MasterClient);
                PhotonNetwork.Destroy(gameObject);
            }
            //Destroy(gameObject);
        }
        
        Debug.Log(gameObject.name + " ded");
    }
}
