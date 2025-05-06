using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

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
        player.gameObject.SetActive(false); // 미니게임 중, 플레이어 오브젝트가 동작하는 것을 막기 위해 비활성화

        SceneManager.LoadScene("FlipingMiniGameScene", LoadSceneMode.Additive);
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
        player.gameObject.SetActive(true); // 미니게임 종료 후, 비활성화 되어있던 플레이어 오브젝트 다시 활성화
        miniGameButton.IsClicked = false;
    }
}
