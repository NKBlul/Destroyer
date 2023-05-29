using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] public float m_Health;
    [SerializeField] public float m_Damage;
    [SerializeField] public float m_Speed;

    protected Rigidbody m_Rigidbody;

    private void Update()
    {
        if (m_Health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Debug.Log("Damage: " + m_Damage);
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Character ded");
    }
}
