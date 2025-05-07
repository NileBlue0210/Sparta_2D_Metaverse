using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private GameManager gameManager = null;

    private MiniGameUIManager uiManager;

    public PlaneController player { get; private set; } // 미니게임에서 조종할 플레이어 오브젝트

    private bool isMiniGamePlay = false;
    public bool IsMiniGamePlay { get { return isMiniGamePlay; } }

    private int currentScore = 0;
    private int bestScore = 0;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        uiManager = FindObjectOfType<MiniGameUIManager>();
        player = FindObjectOfType<PlaneController>();
    }

    private void Start()
    {
        if (uiManager == null)
            Debug.LogError("not founded mini game ui manager");
        if (player == null)
            Debug.LogError("not founded player");

        bestScore = PlayerPrefs.GetInt("MiniGameBestScore", 0);
    }

    public void MiniGameStart()
    {
        if (!gameManager.IsMiniGameActive)
            return;

        uiManager.SetStart();
    }

    public void MiniGameRestart()
    {
        if (!gameManager.IsMiniGameActive)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MiniGameOver()
    {
        if (!gameManager.IsMiniGameActive)
            return;

        // to do: json형식으로 저장하여 보안성을 높이는 것을 고려해보자
        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            PlayerPrefs.SetInt("MiniGameBestScore", bestScore);
        }

        PlayerPrefs.SetInt("MiniGameCurrentScore", currentScore);
        PlayerPrefs.Save();

        uiManager.SetRestart();
    }

    public void MiniGameEnd()
    {
        if (!gameManager.IsMiniGameActive)
            return;

        isMiniGamePlay = false;

        gameManager.HideMiniGame();
    }

    public void PlayMiniGame()
    {
        isMiniGamePlay = true;

        if (uiManager.guidePanel.gameObject.activeSelf)
            uiManager.guidePanel.SetActive(false); // 안내 메세지 비활성화
    }

    public void AddScore(int score)
    {
        currentScore += score;

        uiManager.UpdateScore(currentScore);
    }
}
