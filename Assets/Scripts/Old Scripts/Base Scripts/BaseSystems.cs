//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class BaseSystems : MonoBehaviour
//{
//    [SerializeField] int m_minHealth;
//    [SerializeField] int m_maxHealth;
//    [SerializeField] float m_currentHealth;
//    [SerializeField] float m_damageTaken1;
//    [SerializeField] float m_damageTaken2;
//    [SerializeField] float m_damageTaken3;
//    [SerializeField] bool b_isDamaged;
//    [SerializeField] bool b_isSeverelyDamaged;
//    // Start is called before the first frame update
//    void Start()
//    {
//        m_minHealth = 0;
//        m_maxHealth = 1000;
//        m_currentHealth= m_maxHealth;
      
//        b_isDamaged = false;
//        b_isSeverelyDamaged = false;
//    }

//    public void Destroy()
//    {
//        //
//    }

//    public void Damage(float damageTaken)
//    {
//        m_currentHealth -= damageTaken; 
        
//        if (m_currentHealth <= m_minHealth)
//        {
//            Destroy();
//        }
//    }

//    public void is_Damaged()
//    {
//        if (b_isDamaged) 
//        { 
//            //
//        }

//        if (b_isSeverelyDamaged)
//        {
//            //
//        }
//    }

//    public void ClampHealth()
//    {
//        if (m_currentHealth > m_maxHealth) 
//        { 
//            m_currentHealth = m_maxHealth;
//        }

//        if (m_currentHealth <= m_minHealth)
//        {
//            Destroy();
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        ClampHealth();
//        is_Damaged();   
//    }
//}
