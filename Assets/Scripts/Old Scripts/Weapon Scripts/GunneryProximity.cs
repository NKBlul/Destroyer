using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunneryProximity : MonoBehaviour
{
    public bool inRadius;
    // Start is called before the first frame update
    void Start()
    {
        inRadius = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(inRadius);
    }

    public bool GetInRad()
    {
        return inRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRadius = false;
        }
    }
}
