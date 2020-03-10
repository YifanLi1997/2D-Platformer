using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpPower = 10f;

    float m_XMove;
    bool m_isFacingRight = true;
    Rigidbody2D m_rigidbody2D;

    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Control
        m_XMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Animation

        // Direction
        if (m_XMove < 0f && m_isFacingRight == true)
        {
            FlipPlayer();
        }
        else if (m_XMove > 0f && m_isFacingRight == false)
        {
            FlipPlayer();
        }

        // Physics
        var currentVelocity = m_rigidbody2D.velocity;
        m_rigidbody2D.velocity = new Vector2(m_XMove * playerSpeed * Time.deltaTime, currentVelocity.y);
        //transform.Translate(Vector3.right * m_XMove * playerSpeed * Time.deltaTime);

    }

    private void Jump()
    {
        m_rigidbody2D.AddForce(Vector2.up * jumpPower);
    }

    private void FlipPlayer()
    {
        m_isFacingRight = !m_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
