using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPunCallbacks/*, IDamageable*/
{
    public static PlayerController Instance;

    [SerializeField] Image healthbarImage;
	[SerializeField] GameObject ui;

	[SerializeField] GameObject cameraHolder;

	[SerializeField] GameObject enemy;

	[SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

	public Rigidbody rigidbody;

    // For New Input System
    PlayerControls _controllerInput;

	float verticalLookRotation;
	bool grounded;
    Vector3 smoothMoveVelocity;
	Vector3 moveAmount;

	public GameObject CrossHair;

	Rigidbody rb;

	PhotonView PV;

	const float maxHealth = 100f;
	float currentHealth = maxHealth;

	PlayerManager playerManager;

    // New Input System variables
    Vector2 _movement, _lookDir;

	void Awake()
	{
		Instance = this;

		rb = GetComponent<Rigidbody>();
		PV = GetComponent<PhotonView>();

		playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();

		_controllerInput = new PlayerControls();

		_controllerInput.Default.Jump.performed += ctx => Jump();

		_controllerInput.Default.Move.performed += ctx => _movement = ctx.ReadValue<Vector2>();
		_controllerInput.Default.Move.canceled += ctx => _movement = Vector2.zero;

		_controllerInput.Default.LookX.performed += ctx => _lookDir.x = ctx.ReadValue<float>();
        _controllerInput.Default.LookX.canceled += ctx => _lookDir.x = 0;

        _controllerInput.Default.LookY.performed += ctx => _lookDir.y = ctx.ReadValue<float>();
        _controllerInput.Default.LookY.canceled += ctx => _lookDir.y = 0;
    }

	void Start()
	{
		if(PV.IsMine)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = true;
		}
		else
		{
			Destroy(GetComponentInChildren<Camera>().gameObject);
			Destroy(rb);
			Destroy(ui);
		}
	}

	void Update()
	{
		if(!PV.IsMine)
			return;

        if (ChangeCamera.Instance.playerCam == true)
        {
            Move();
            Look();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            CrossHair.SetActive(true);
        }
        else if (ChangeCamera.Instance.minigameCam == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CrossHair.SetActive(false);
        }
        else if (ChangeCamera.Instance.shipCam == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            CrossHair.SetActive(false);
        }
        else if (ChangeCamera.Instance.turretCam == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            CrossHair.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

		if(transform.position.y < -10f) // Die if you fall out of the world
		{
			Die();
		}
    }

	void Look()
	{
        transform.Rotate(Vector3.up * _lookDir.x * mouseSensitivity);

		verticalLookRotation += _lookDir.y * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
	{
        Vector3 moveDir = new Vector3(_movement.x, 0, _movement.y);

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (_controllerInput.Default.Run.IsPressed() ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void Jump()
	{
		if (ChangeCamera.Instance.playerCam == true)
		{
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);

            }
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

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    void FixedUpdate()
	{
		if(!PV.IsMine)
			return;

		rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}

	void Die()
	{
		playerManager.Die();
	}
}