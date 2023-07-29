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
        Debug.Log("groundcheck Entered");
        if (other.gameObject == playerController.gameObject)
            return;

        playerController.SetGroundedState(true);
    }

    void OnTriggerExit(Collider other)
    {
		Debug.Log("groundcheck Exit");
		if (other.gameObject == playerController.gameObject)
            return;

        playerController.SetGroundedState(false);
    }
}