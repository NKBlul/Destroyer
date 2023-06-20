using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGrid : BaseSystems
{
    [SerializeField] float currPower;
    float maxPower;
    float drainsCurrPercent;
    float lightsCurrPercent;
    float autoloadersCurrPercent;
    float sonarCurrPercent;
    // Start is called before the first frame update
    void Start()
    {
        currPower = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
