using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private GameManager gameManager = null;

    private MiniGameUIManager uiManager;

    public PlaneController player { get; private set; } // �̴ϰ��ӿ��� ������ �÷��̾� ������Ʈ

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

        // player.gameObject.SetActive(false); // �̴ϰ��� �� �ε� ��, ������ ���۵Ǳ� ������ �÷��̾� ������Ʈ ��Ȱ��ȭ

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

        // to do: json�������� �����Ͽ� ���ȼ��� ���̴� ���� ����غ���
        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            PlayerPrefs.SetInt("MiniGameBestScore", bestScore);
            PlayerPrefs.Save();
        }

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

        if (!player.gameObject.activeSelf)  // �ʱ� ���� ��, �÷��̾� ������Ʈ�� ��Ȱ��ȭ �Ǿ����� ��� �÷��̾� ������Ʈ�� Ȱ��ȭ
            player.gameObject.SetActive(true);

        if (uiManager.guidePanel.gameObject.activeSelf)
            uiManager.guidePanel.SetActive(false); // �ȳ� �޼��� ��Ȱ��ȭ
    }

    public void AddScore(int score)
    {
        currentScore += score;

        uiManager.UpdateScore(currentScore);
    }
}
