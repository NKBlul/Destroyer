using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSystems : MonoBehaviour
{
    [SerializeField] public float m_Health = 100.0f;
    private float m_MaxHealth = 100.0f;
    [SerializeField] public float m_DamageDealt = 20.0f;

    public static BaseSystems instance;

    private void Awake()
    {
        instance = this;
        m_Health = m_MaxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ship: " + m_Health);
        //CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
        CheckHealth();     
    }

    private void CheckHealth() 
    {
        UI.instance.hpBar.fillAmount = m_Health / m_MaxHealth;
        if (m_Health <= m_MaxHealth / 2)
        {
            //warning sign pop up
            UI.instance.warning.gameObject.SetActive(true);
        }
        if (m_Health <= 0)
        {
            Debug.Log("ded");
            //Destroy(gameObject);
        }
    }

    public void RecoverHealth()
    {
        m_Health = m_MaxHealth;
        UI.instance.warning.gameObject.SetActive(false);
    }
}
