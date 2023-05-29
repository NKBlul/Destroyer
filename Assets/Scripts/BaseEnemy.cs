using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    [SerializeField] string m_Type;
    // Start is called before the first frame update
    void Start()
    {
        m_Health = 100f;
        m_Damage = 25f;
        m_Speed = 50f;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
