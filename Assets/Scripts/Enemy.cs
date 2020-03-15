using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] float detectionThreshold = 0.7f;

    Rigidbody2D m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.velocity = Vector2.right * speed;
    }


    void Update()
    {
        Debug.Log(m_rb.velocity);

        RaycastHit2D xHit;
        if (isFacingRight)
        {
            xHit = Physics2D.Raycast(transform.position, Vector2.right);
            if (xHit.distance < detectionThreshold)
            {
                isFacingRight = !isFacingRight;
                m_rb.velocity = Vector2.left * speed;
            }
        }
        else
        {
            xHit = Physics2D.Raycast(transform.position, Vector2.left);
            if (xHit.distance < detectionThreshold)
            {
                isFacingRight = !isFacingRight;
                m_rb.velocity = Vector2.right * speed;
            }
        }


    }
}
