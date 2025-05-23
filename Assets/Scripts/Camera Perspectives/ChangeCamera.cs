using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeCamera : MonoBehaviour
{
    public static ChangeCamera Instance;

    public Camera[] cameras;  // Array to hold your cameras
    private int currentCameraIndex;  // Index of the current active camera

    //public GameObject turret;
    MoveShip ship;
    PlayerController player;
    MainTurret turret;

    public bool playerCam;
    public bool shipCam;
    public bool turretCam;
    public bool minigameCam;

    // For New Input System
    PlayerControls _controllerInput;

    private void Awake()
    {
        Instance = this;

        _controllerInput = new PlayerControls();

        _controllerInput.Default.ChangeCamera.performed += ctx => SwitchCamera();
    }

    private void Start()
    {
        turret = FindObjectOfType<MainTurret>();
        player = FindAnyObjectByType<PlayerController>();
        ship = FindObjectOfType<MoveShip>();
        //pc = GetComponent<PlayerController>().GetInRadius();
        // Set the first camera as the starting camera
        currentCameraIndex = 0;
        //ActivateCamera(currentCameraIndex);       
        
        ActivateCamera(currentCameraIndex);
    }

    private void Update()
    {

        // Check for key input to switch cameras
        //if (Input.GetKeyDown(KeyCode.T) && 
        //    FindObjectOfType<GunneryProximity>().GetInRad() == true)
        //{
        //    SwitchCamera();
        //}
        CheckCamera();
        DisablePlayerCanvas();
        print(shipCam);
    }

    public void SwitchCamera()
    {
        if (FindObjectOfType<GunneryProximity>().GetInRad() == true)
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
    }

    private void OnEnable()
    {
        _controllerInput.Default.Enable();
    }

    private void OnDisable()
    {
        _controllerInput.Default.Disable();
    }

    private void ActivateCamera(int index)
    {
        // Activate the specified camera
        cameras[index].gameObject.SetActive(true);
        if (cameras[index].gameObject.tag == "turret" && turret != null)
        {
            turret.GetComponent<MainTurret>().SetActive();

            //cameras[index].enabled = true;
            //Camera.main.enabled = false;
            //player.GetComponent<PlayerController>().enabled = false;
        }
        else if (!(cameras[index].gameObject.tag == "turret") && turret != null)
        {
            turret.GetComponent<MainTurret>().SetNotActive();
            //cameras[index].enabled = false;
            //Camera.main.enabled = true;
            //player.GetComponent<PlayerController>().enabled = true;
        }

        //if ((currentCameraIndex == 0) )
        //{
        //    player.GetComponent<PlayerController>().enabled = true;
        //    turret.GetComponent<MainTurret>().enabled = false;
        //    ship.GetComponent<MoveShip>().enabled = false;
        //}
        //
        //else if ((currentCameraIndex == 1))
        //{
        //    player.GetComponent<PlayerController>().enabled = false;
        //    turret.GetComponent<MainTurret>().enabled = false;
        //    ship.GetComponent<MoveShip>().enabled = true;
        //}
        //
        //else if ((currentCameraIndex == 2))
        //{
        //    player.GetComponent<PlayerController>().enabled = false;
        //    turret.GetComponent<MainTurret>().enabled = true;
        //    ship.GetComponent<MoveShip>().enabled = false;
        //}
        //
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
        //
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
        //
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

    private void CheckCamera()
    {
        if (cameras[currentCameraIndex] == cameras[0]) //player cam
        {
            playerCam = true;
            shipCam = false;
            turretCam = false;
            minigameCam = false;
        }
        else if (cameras[currentCameraIndex] == cameras[1]) //ship cam
        {
            playerCam = false;
            shipCam = true;
            turretCam = false;
            minigameCam = false;
            if (Input.GetKeyDown(KeyCode.W))
            {
                FindObjectOfType<MoveShip>().IncreaseShipSpeed();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                FindObjectOfType<MoveShip>().DecreaseShipSpeed();
            }
        }
        else if (cameras[currentCameraIndex] == cameras[2]) //turret cam
        {
            playerCam = false;
            shipCam = false;
            turretCam = true;
            minigameCam = false;
        }
        else if (cameras[currentCameraIndex] == cameras[3]) //minigame cam
        {
            playerCam = false;
            shipCam = false;
            turretCam = false;
            minigameCam = true;
        }
    }

    private void DisablePlayerCanvas()
    {
        //if (playerCam == false)
        //{
        //    player.GetComponentInChildren<Image>().enabled = false;
        //}
        //else
        //{
        //    player.GetComponentInChildren<Image>().enabled = true;
        //}
    }
}


