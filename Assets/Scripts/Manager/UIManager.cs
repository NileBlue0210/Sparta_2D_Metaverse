using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        resultPanel.SetActive(false);
    }

    void Start()
    {
        if (resultPanel == null)
            Debug.LogError("result panel is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        if (bestScoreText == null)
            Debug.LogError("best score text is null");

        bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("MiniGameBestScore", 0).ToString()}";
        scoreText.text = $"Current Score: {PlayerPrefs.GetInt("MiniGameCurrentScore", 0).ToString()}";
    }

    public void ShowResultPanel()
    {
        if (!resultPanel.activeSelf)
        {
            resultPanel.SetActive(true);
        }
    }

    public void HideResultPanel()
    {
        if (resultPanel.activeSelf)
        {
            resultPanel.SetActive(false);
        }
    }
}
