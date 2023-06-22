using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField]float damage;
    [SerializeField]float projectileSpeed;

    [SerializeField]int distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDistance(int distance)
    {
        this.distance = distance;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetProjectileSpeed(float projectileSpeed)
    {
        this.projectileSpeed = projectileSpeed;
    }

    public int GetDistance()
    { 
        return this.distance; 
    }

    public float GetDamage()
    {
        return this.damage;
    }

    public float GetProjectileSpeed()
    {
        return this.projectileSpeed;
    }
}
