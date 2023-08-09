using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    static public Minigame Instance;

    public int wireCount;
    private int onCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void WireConnected(int count)
    {
        onCount += count;
        if (onCount == wireCount)
        {
            //all connected
            BaseSystems.instance.RecoverHealth();
        }
    }
}
