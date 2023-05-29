using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    [SerializeField] float m_Health;
    [SerializeField] float m_Damage;
    [SerializeField] float m_Speed;
    [SerializeField] string m_Role;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = 100f;
        m_Damage = 50f;
        m_Speed = 75f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
