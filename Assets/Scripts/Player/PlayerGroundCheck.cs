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
		//if(other.gameObject == playerController.gameObject)
		//	return;

		if (other.gameObject.CompareTag("Ground"))
			playerController.SetGroundedState(true);
	}

	void OnTriggerExit(Collider other)
	{
		//if(other.gameObject == playerController.gameObject)
		//	return;

        if (other.gameObject.CompareTag("Ground"))
            playerController.SetGroundedState(false);
	}

	void OnTriggerStay(Collider other)
	{
		//if(other.gameObject == playerController.gameObject)
		//	return;

        if (other.gameObject.CompareTag("Ground"))
            playerController.SetGroundedState(true);
	}
}