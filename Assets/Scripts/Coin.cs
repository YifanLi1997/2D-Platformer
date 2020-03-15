using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip pickClip;
    [SerializeField] int bonusPoint = 10;

    ScoreSystem m_scoreSystem;

    void Start()
    {
        m_scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_scoreSystem.AddScore(bonusPoint);
            PlayPickAudio();
            Destroy(gameObject);
        }
    }

    public void PlayPickAudio()
    {
        AudioSource.PlayClipAtPoint(pickClip, Camera.main.transform.position);
    }

    public int GetBonusPoint()
    {
        return bonusPoint;
    }
}
