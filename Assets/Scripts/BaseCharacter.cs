using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] public float m_Health = 100.0f;
    [SerializeField] public float m_DamageDealt = 20.0f;
    [SerializeField] public float m_Speed = 5.0f;
    [SerializeField] string m_CharacterRole = "Null";

    protected Rigidbody m_Rigidbody;

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
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Character ded");
    }
}
