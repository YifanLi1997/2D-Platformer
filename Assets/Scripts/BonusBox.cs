using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    [SerializeField] GameObject bonusPrefab;
    [SerializeField] float raycastThreshold = 0.7f;

    Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit2D yDownHit = Physics2D.Raycast(transform.position, Vector2.down);

        if (yDownHit && yDownHit.distance < raycastThreshold)
        {
            if (yDownHit.collider.gameObject.CompareTag("Player"))
            {
                GameObject bonus = Instantiate(
                    bonusPrefab,
                    gameObject.transform.position + Vector3.up,
                    Quaternion.identity) as GameObject;
                m_animator.SetBool("isExploded", true);
                Destroy(gameObject, 1f);
            }
        }
    }

}
