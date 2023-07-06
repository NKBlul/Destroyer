using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.InputSystem;
using static UnityEditorInternal.ReorderableList;

public class PlayerController : MonoBehaviourPunCallbacks/*, IDamageable*/
{
    public static PlayerController Instance;

    [SerializeField] Image healthbarImage;
	[SerializeField] GameObject ui;

	[SerializeField] GameObject cameraHolder;

	[SerializeField] GameObject enemy;

	[SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

	//[SerializeField] Item[] items;
	public Rigidbody rigidbody;

	// For controller input system
	PlayerControls _controllerInput;

	//int itemIndex;
	//int previousItemIndex = -1;

	float verticalLookRotation;
	bool grounded;
    Vector3 smoothMoveVelocity;
	Vector3 moveAmount;

	Rigidbody rb;

	PhotonView PV;

	const float maxHealth = 100f;
	float currentHealth = maxHealth;

	PlayerManager playerManager;

	// Controller variables
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
			//EquipItem(0);
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

		Look();
		Move();
		//Jump();

		//for(int i = 0; i < items.Length; i++)
		//{
		//	if(Input.GetKeyDown((i + 1).ToString()))
		//	{
		//		EquipItem(i);
		//		break;
		//	}
		//}

		//if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
		//{
		//	if(itemIndex >= items.Length - 1)
		//	{
		//		EquipItem(0);
		//	}
		//	else
		//	{
		//		EquipItem(itemIndex + 1);
		//	}
		//}
		//else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
		//{
		//	if(itemIndex <= 0)
		//	{
		//		EquipItem(items.Length - 1);
		//	}
		//	else
		//	{
		//		EquipItem(itemIndex - 1);
		//	}
		//}

		//if(Input.GetMouseButtonDown(0))
		//{
		//	items[itemIndex].Use();
		//}

		if(transform.position.y < -10f) // Die if you fall out of the world
		{
			Die();
		}
    }

	void Look()
	{
        //transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        //verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        //cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;


		// Controller
        transform.Rotate(Vector3.up * _lookDir.x * mouseSensitivity);

		verticalLookRotation += _lookDir.y * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
	{
		//Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

		//moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
		//rigidbody.MovePosition(transform.position + moveAmount * Time.deltaTime);


		// Controller
		Vector3 moveDir = new Vector3(_movement.x, 0, _movement.y);

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (_controllerInput.Default.Run.IsPressed() ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        rigidbody.MovePosition(transform.position + moveAmount * Time.deltaTime);
    }

    void Jump()
	{
		//if(Input.GetKeyDown(KeyCode.Space) && grounded)
		//{
		//	rb.AddForce(transform.up * jumpForce);
		//}


		// Controller
        if (grounded)
        {
            rb.AddForce(transform.up * jumpForce);
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

    //void EquipItem(int _index)
    //{
    //	if(_index == previousItemIndex)
    //		return;
    //
    //	itemIndex = _index;
    //
    //	items[itemIndex].itemGameObject.SetActive(true);
    //
    //	if(previousItemIndex != -1)
    //	{
    //		items[previousItemIndex].itemGameObject.SetActive(false);
    //	}
    //
    //	previousItemIndex = itemIndex;
    //
    //	if(PV.IsMine)
    //	{
    //		Hashtable hash = new Hashtable();
    //		hash.Add("itemIndex", itemIndex);
    //		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    //	}
    //}

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    //{
    //	if(changedProps.ContainsKey("itemIndex") && !PV.IsMine && targetPlayer == PV.Owner)
    //	{
    //		EquipItem((int)changedProps["itemIndex"]);
    //	}
    //}

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

	//public void TakeDamage(float damage)
	//{
	//	PV.RPC(nameof(RPC_TakeDamage), PV.Owner, damage);
	//}
	//
	//[PunRPC]
	//void RPC_TakeDamage(float damage, PhotonMessageInfo info)
	//{
	//	currentHealth -= damage;
	//
	//	healthbarImage.fillAmount = currentHealth / maxHealth;
	//
	//	if(currentHealth <= 0)
	//	{
	//		Die();
	//		PlayerManager.Find(info.Sender).GetKill();
	//	}
	//}

	void Die()
	{
		playerManager.Die();
	}
}