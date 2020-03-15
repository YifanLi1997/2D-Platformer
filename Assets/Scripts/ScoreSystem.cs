using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        DisplayText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int point)
    {
        score += point;
        DisplayText();
    }

    public void DisplayText()
    {
        text.text = "Score: " + score.ToString();
    }
}
