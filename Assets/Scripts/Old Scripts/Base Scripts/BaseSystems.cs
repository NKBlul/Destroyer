using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSystems : MonoBehaviour
{
    [SerializeField] public float m_Health = 100.0f;
    [SerializeField] public float m_DamageDealt = 20.0f;

    public static BaseSystems instance;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        CheckHealth();     
    }

    private void CheckHealth() 
    {
        if (m_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
