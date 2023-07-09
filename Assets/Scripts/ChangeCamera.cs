using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera[] cameras;  // Array to hold your cameras
    private int currentCameraIndex;  // Index of the current active camera

    MainTurret mainTurret;

   
    private void Start()
    {
        // Set the first camera as the starting camera
        currentCameraIndex = 0;
        ActivateCamera(currentCameraIndex);
        
        mainTurret = FindObjectOfType<MainTurret>();
    }

    private void Update()
    {

        // Check for key input to switch cameras
        if (Input.GetKeyDown(KeyCode.T))
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
            
            cameras[index].enabled= true;
            Camera.main.enabled = false;
        }
        else if (!(cameras[index].gameObject.tag == "turret") && mainTurret != null)
        {
            mainTurret.GetComponent<MainTurret>().SetNotActive();
            cameras[index].enabled= false;
            Camera.main.enabled = true;
        }
    }
}

