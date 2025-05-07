using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private MiniGameUIManager miniGameUIManager;

    private bool isMiniGameActive = false;
    public bool IsMiniGameActive { get { return isMiniGameActive; } }

    public MiniGameButtonController miniGameButton { get; private set; }
    public PlayerController player { get; private set; }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<PlayerController>();
        miniGameUIManager = FindObjectOfType<MiniGameUIManager>();  // �ٸ� ���� �ִ� ������ư�� ã�� �� �ֳ�?
    }

    public void StartMiniGame()
    {
        if (isMiniGameActive)
            return;

        ShowMiniGame();
    }

    private void ShowMiniGame()
    {
        isMiniGameActive = true;
        player.gameObject.SetActive(false); // �̴ϰ��� ��, �÷��̾� ������Ʈ�� �����ϴ� ���� ���� ���� ��Ȱ��ȭ

        // SceneManager.LoadScene("FlappingMiniGameScene", LoadSceneMode.Additive); // to do: ���� Additive�ɼ��� ����� �˾� â���� �����ϴ� �������� �����غ� ��
        SceneManager.LoadScene("FlappingMiniGameScene");
    }

    public void EndMiniGame()
    {
        if (!isMiniGameActive)
            return;

        HideMiniGame();
    }

    private void HideMiniGame()
    {
        SceneManager.UnloadSceneAsync("FlipingMiniGameScene");

        isMiniGameActive = false;
        player.gameObject.SetActive(true); // �̴ϰ��� ���� ��, ��Ȱ��ȭ �Ǿ��ִ� �÷��̾� ������Ʈ �ٽ� Ȱ��ȭ
        miniGameButton.IsClicked = false;
    }

    public void MiniGameOver()
    {
        if (!isMiniGameActive)
            return;

        miniGameUIManager.SetRestart();
    }

    public void MiniGameRestart()
    {
        if (!isMiniGameActive)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MiniGameEnd()
    {
        if (!isMiniGameActive)
            return;

        HideMiniGame();
    }
}
