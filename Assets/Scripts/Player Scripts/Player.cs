using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region singletone
    public static Player instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] Transform _playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float _mouseSmoothTime = 0.03f;
    [SerializeField] bool _cursorLock = true;
    [SerializeField] float _mouseSensitivity = 3.5f;
    [SerializeField] float _speed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float _moveSmoothTime = 0.3f;
    [SerializeField] float _gravity = -30.0f;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _ground;

    public float _jumpHeight = 6.0f;
    float _velocityY;
    bool _isGrounded;

    float _cameraCap;
    Vector2 _currentMouseDelta;
    Vector2 _currentMouseDeltaVelocity;

    CharacterController _controller;
    Vector2 _currentDir;
    Vector2 _currentDirVelocity;

    public GameObject camera;

    public void Init()
    {
		_controller = GetComponent<CharacterController>();

        if (_cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        camera.SetActive(true);
    }

	public void Move() 
    {
		// Mouse
		Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		_currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, _mouseSmoothTime);

		_cameraCap -= _currentMouseDelta.y * _mouseSensitivity;

		_cameraCap = Mathf.Clamp(_cameraCap, -90.0f, 90.0f);

		_playerCamera.localEulerAngles = Vector3.right * _cameraCap;

		transform.Rotate(Vector3.up * _currentMouseDelta.x * _mouseSensitivity);
        // Mouse


        
		// Move
		_isGrounded = Physics.CheckSphere(_groundCheck.position, 0.2f, _ground);

		Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		targetDir.Normalize();

		_currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentDirVelocity, _moveSmoothTime);

		_velocityY += _gravity * 2.0f * Time.deltaTime;

		Vector3 velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _speed + Vector3.up * _velocityY;

		_controller.Move(velocity * Time.deltaTime);

		if (_isGrounded && Input.GetButtonDown("Jump")) {
			_velocityY = Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
		}

		if (_isGrounded! && _controller.velocity.y < -1.0f) {
			_velocityY = -8.0f;
		}

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 10.0f;
        }
        else
        {
            _speed = 6.0f;
        }
		// Move
	}
}
