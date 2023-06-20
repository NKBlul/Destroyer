using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munitions : BaseSystems
{
    [SerializeField] float turretMaxCap;
    [SerializeField] float turretCurrentAmt;
    [SerializeField] float reloadTime; 
    // Start is called before the first frame update
    void Start()
    {
        turretCurrentAmt = turretMaxCap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
