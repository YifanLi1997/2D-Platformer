using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpPower = 300f;
    [Tooltip("Bounce after killing an enemy")] [SerializeField] float bouncePower = 100f;
    [SerializeField] float minVelocity = -3.5f;
    [SerializeField] float maxVelocity = 3.5f;

    [Header("Inner params")]
    [SerializeField] float raycastThreshold = 0.7f;
    [SerializeField] GameObject deathPanel;

    float m_moveInput;
    bool m_isFacingRight = true;
    bool m_isReadyToJump = false;
    Rigidbody2D m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DeathDetection();
        RaycastDetection();

        // TODO: change to a button
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;
        }
    }

    void FixedUpdate()
    {
        GetInput();
        DirectionControl();
        MovementControl();
    }

    private void RaycastDetection()
    {
        // kill enemy
        RaycastHit2D YHit = Physics2D.Raycast(transform.position,  Vector2.down);

        if (YHit.distance < raycastThreshold && YHit.collider.CompareTag("Enemy"))
        {
            YHit.collider.GetComponent<Enemy>().EnemyKilled();
            m_rb.AddForce(Vector2.up * bouncePower);
        }
    }

    private void DeathDetection()
    {
        if (transform.position.y < -5.5)
        {
            PlayerDeath();
        }
    }


    public void SetIsReadyToJump(bool isReady)
    {
        m_isReadyToJump = isReady;
    }

    private void GetInput()
    {
        m_moveInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void DirectionControl()
    {
        if (m_moveInput < 0f && m_isFacingRight == true)
        {
            FlipPlayer();
        }
        else if (m_moveInput > 0f && m_isFacingRight == false)
        {
            FlipPlayer();
        }
    }

    private void MovementControl()
    {
        if (Mathf.Abs(m_moveInput) > 0.6f)
        {
            m_rb.AddForce(Vector2.right * m_moveInput * playerSpeed);
            if (m_moveInput < 0f)
            {
                SetSpeedLimitsTowardsLeft();
            }
            else if (m_moveInput > 0f)
            {
                SetSpeedLimitsTowardsRight();
            }
        }
    }

    private void SetSpeedLimitsTowardsRight()
    {
        if (m_rb.velocity.x > maxVelocity)
        {
            m_rb.velocity = new Vector2(maxVelocity, m_rb.velocity.y);
        }
    }

    private void SetSpeedLimitsTowardsLeft()
    {
        if (m_rb.velocity.x < minVelocity)
        {
            m_rb.velocity = new Vector2(minVelocity, m_rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (m_isReadyToJump)
        {
            m_rb.AddForce(Vector2.up * jumpPower);
            m_isReadyToJump = false;
        }
    }

    private void FlipPlayer()
    {
        m_isFacingRight = !m_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void PlayerDeath()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
