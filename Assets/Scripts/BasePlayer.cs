using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseCharacter
{   
    [SerializeField] string m_Role;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = 100f;
        m_Damage = 50f;
        m_Speed = 75f;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(20);
            Debug.Log(m_Health);
            m_Rigidbody.AddForce(Vector3.up, ForceMode.Impulse);    
        }
    }
}
