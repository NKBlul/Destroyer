using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comms : BaseSystems
{
    bool isRecievingMission;
    bool isRecievingEvent;
    bool isRecievingMessage;
    string textFromComms;
    // Start is called before the first frame update
    void Start()
    {
        isRecievingMessage= false;
        isRecievingMission= false;
        isRecievingEvent= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
