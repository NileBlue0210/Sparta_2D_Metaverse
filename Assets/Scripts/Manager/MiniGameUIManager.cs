using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameUIManager : MonoBehaviour
{
    public GameObject guidePanel;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");

        if (scoreText == null)
            Debug.LogError("score text is null");

        if (bestScoreText == null)
            Debug.LogError("best score text is null");

        bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("MiniGameBestScore", 0).ToString()}";    // �� ��, ���. �ӽ�, Ȯ��.
    }

    // �� �ε� �� �� �޼ҵ带 ȣ���� �ȳ� �޼����� �������� ����. �÷��̾ Ŭ���̳� �����̽��� �Է��ϸ� �޼����� �������, Plane������Ʈ�� Ȱ��ȭ�ǵ��� ����
    public void SetStart()
    {
        guidePanel.SetActive(true);
        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score.ToString()}";
    }
}
