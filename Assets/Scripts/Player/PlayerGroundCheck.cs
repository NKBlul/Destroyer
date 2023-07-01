using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
	PlayerController playerController;

	void Awake()
	{
		playerController = GetComponentInParent<PlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player on Platform");
        if (other.CompareTag("Platform"))
        {
            playerController.SetGroundedState(true);
        }

        //if (other.gameObject == playerController.gameObject)
        //return;

        //playerController.SetGroundedState(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            playerController.SetGroundedState(false);
        }
        //if (other.gameObject == playerController.gameObject)
        //    return;

        //playerController.SetGroundedState(false);
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject == playerController.gameObject)
    //        return;

    //    playerController.SetGroundedState(true);
    //}
}