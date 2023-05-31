using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerController _controller;

    public GameObject _camera;

    public void IsLocalPlayer()
    {
        _controller.enabled = true;
        _camera.SetActive(true);
    }
}
