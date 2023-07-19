using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera[] cameras;  // Array to hold your cameras
    private int currentCameraIndex;  // Index of the current active camera

    //public GameObject turret;
    public GameObject player;
    //public GameObject ship;
    MainTurret mainTurret;

    bool pc;

    private void Start()
    {
        //pc = GetComponent<PlayerController>().GetInRadius();
        // Set the first camera as the starting camera
        currentCameraIndex = 0;
        ActivateCamera(currentCameraIndex);
        
        mainTurret = FindObjectOfType<MainTurret>();
        //player = FindAnyObjectByType<PlayerController>();
        //ship = FindObjectOfType<MoveShip>();
    }

    private void Update()
    {

        // Check for key input to switch cameras
        if (Input.GetKeyDown(KeyCode.T) /*&& pc == true*/)
        {
            SwitchCamera();
        }

    }

    public void SwitchCamera()
    {
        // Deactivate the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Increment the camera index
        currentCameraIndex++;

        // Wrap around to the first camera if the index goes beyond the array size
        if (currentCameraIndex >= cameras.Length)
            currentCameraIndex = 0;

        // Activate the new camera
        ActivateCamera(currentCameraIndex);
    }

    private void ActivateCamera(int index)
    {
        // Activate the specified camera
        cameras[index].gameObject.SetActive(true);
        if (cameras[index].gameObject.tag == "turret" && mainTurret != null)
        {
            mainTurret.GetComponent<MainTurret>().SetActive();

            cameras[index].enabled = true;
            //Camera.main.enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
        }
        else if (!(cameras[index].gameObject.tag == "turret") && mainTurret != null)
        {
            mainTurret.GetComponent<MainTurret>().SetNotActive();
            cameras[index].enabled = false;
            //Camera.main.enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
        }

        //if ((currentCameraIndex == 0) && turret != null)
        //{
        //    player.GetComponent<PlayerController>().enabled = true;
        //    turret.GetComponent<MainTurret>().enabled = false;
        //    ship.GetComponent<MoveShip>().enabled = false;
        //}

        //else if ((currentCameraIndex == 1) && turret != null)
        //{
        //    player.GetComponent<PlayerController>().enabled = false;
        //    turret.GetComponent<MainTurret>().enabled = false;
        //    ship.GetComponent<MoveShip>().enabled = true;
        //}

        //else if ((currentCameraIndex == 2) && turret != null)
        //{
        //    player.GetComponent<PlayerController>().enabled = false;
        //    turret.GetComponent<MainTurret>().enabled = true;
        //    ship.GetComponent<MoveShip>().enabled = false;
        //}

        //if (index == 2) // Assuming the main turret camera is at index 2
        //{
        //    // Disable player movement script
        //    if (player != null)
        //    {
        //        PlayerController playerMovement = player.GetComponent<PlayerController>();
        //        if (playerMovement != null)
        //        {
        //            playerMovement.enabled = false;
        //        }
        //    }

        //    // Disable ship movement script
        //    if (turret != null)
        //    {
        //        MainTurret turretMovement = turret.GetComponent<MainTurret>();
        //        if (turretMovement != null)
        //        {
        //            turretMovement.enabled = false;
        //        }
        //    }
        //}
        //else
        //{
        //    // Enable player movement script
        //    if (player != null)
        //    {
        //        PlayerController playerMovement = player.GetComponent<PlayerController>();
        //        if (playerMovement != null)
        //        {
        //            playerMovement.enabled = true;
        //        }
        //    }

        //    // Enable ship movement script
        //    if (turret != null)
        //    {
        //        MainTurret turretMovement = ship.GetComponent<MainTurret>();
        //        if (turretMovement != null)
        //        {
        //            turretMovement.enabled = true;
        //        }
        //    }
        //}


    }
}


