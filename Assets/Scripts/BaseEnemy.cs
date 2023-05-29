using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] float m_damage;
    [SerializeField] float m_health;
    [SerializeField] float m_speed;
    [SerializeField] string m_type;
    // Start is called before the first frame update
    void Start()
    {
        m_damage = 25f;
        m_health= 100f;
        m_speed= 50f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
