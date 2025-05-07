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

        bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("MiniGameBestScore", 0).ToString()}";    // 둘 데, 곤란. 임시, 확정.
    }

    // 씬 로드 시 이 메소드를 호출해 안내 메세지가 나오도록 설정. 플레이어가 클릭이나 스페이스를 입력하면 메세지가 사라지고, Plane오브젝트가 활성화되도록 설정
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
