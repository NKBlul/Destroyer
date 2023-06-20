using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : BaseSystems
{
    [SerializeField] int overclockRate;
    int overclockDisableTime;
    [SerializeField] float shipSpeed;
    float minSpeed;
    float maxSpeed;
    float maxSpeed_OC;
    bool isTooDamaged;
    bool isOverclocking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
