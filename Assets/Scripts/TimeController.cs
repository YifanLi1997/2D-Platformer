using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField] float timeLeftInSecond = 120;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject timeOutPanel;
    [SerializeField] GameObject winPanel;

    ScoreSystem m_scoreSystem;

    void Start()
    {
        timeText.text = "Time: " + Mathf.RoundToInt(timeLeftInSecond).ToString();
        m_scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    void Update()
    {
        timeLeftInSecond -= Time.deltaTime;
        timeText.text = "Time: " + Mathf.RoundToInt(timeLeftInSecond).ToString();

        if (timeLeftInSecond <= 0)
        {
            TimeOut();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;
        }
    }

    void TimeOut()
    {
        Time.timeScale = 0;
        timeOutPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_scoreSystem.AddScore(Mathf.RoundToInt(timeLeftInSecond * 10));
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }
}
