using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] int isFacingRight = 1;
    [SerializeField] float raycastThreshold = 0.7f;
    [SerializeField] int deathScore = 10;
    [SerializeField] AudioClip deathSFX;

    [SerializeField] float deathDelayInSecond = 10f;

    Rigidbody2D m_rb;
    BoxCollider2D m_boxCollider;
    ScoreSystem m_scoreSystem;

    void Start()
    {
        m_scoreSystem = FindObjectOfType<ScoreSystem>();
        m_rb = GetComponent<Rigidbody2D>();
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_rb.velocity = Vector2.right * speed;
    }


    void Update()
    {
        RaycastHit2D xHit = Physics2D.Raycast(transform.position, new Vector2(isFacingRight, 0));

        if (xHit && xHit.distance < raycastThreshold)
        {
            if (xHit.collider.CompareTag("Player"))
            {
                xHit.collider.GetComponent<Player>().PlayerDeath();
            }
            isFacingRight *= -1;
            m_rb.velocity = new Vector2(isFacingRight, 0) * speed;
        }
    }

    public void EnemyKilled()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        m_scoreSystem.AddScore(deathScore);

        m_rb.velocity = new Vector2(m_rb.velocity.x, -1 * speed);
        m_boxCollider.enabled = false;

        Destroy(gameObject, deathDelayInSecond);
    }

}
