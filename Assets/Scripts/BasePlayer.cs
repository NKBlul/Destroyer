using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseCharacter
{
    private Vector3 movement;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        movement = new Vector3(horizontalInput, 0f, VerticalInput) * m_Speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
