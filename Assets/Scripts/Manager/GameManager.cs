using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// to do: 미니게임을 관리하는 매니저를 따로 만드는 것이 맞는 것 같다
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private UIManager uiManager;

    public MiniGameButtonController miniGameButton { get; private set; }
    public MiniGameManager miniGameManager { get; private set; }
    public PlayerController player { get; private set; }

    private bool isMiniGameActive = false;
    public bool IsMiniGameActive { get { return isMiniGameActive; } }

    private bool _showResultOnLoad = false; // 씬 로드시에 결과 패널 표시 플래그

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
        miniGameButton = FindObjectOfType<MiniGameButtonController>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void ShowMiniGame()
    {
        if (isMiniGameActive)
            return;

        isMiniGameActive = true;
        player.gameObject.SetActive(false); // 미니게임 중, 플레이어 오브젝트가 동작하는 것을 막기 위해 비활성화

        // SceneManager.LoadScene("FlappingMiniGameScene", LoadSceneMode.Additive); // to do: 추후 Additive옵션을 사용해 팝업 창에서 실행하는 느낌으로 개선해볼 것
        SceneManager.LoadScene("FlappingMiniGameScene");
    }

    public void HideMiniGame()
    {
        if (!isMiniGameActive)
            return;

        _showResultOnLoad = true;

        SceneManager.LoadScene("MainScene");

        isMiniGameActive = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 미니게임 씬 로드 시, 미니게임의 매니저를 탐색 후 미니게임 시작 함수를 호출한다
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FlappingMiniGameScene")
        {
            miniGameManager = FindObjectOfType<MiniGameManager>();

            if (miniGameManager != null)
            {
                miniGameManager.MiniGameStart();
            }
            else
            {
                Debug.LogWarning("Can not find MiniGameManager.");
            }
        }

        if (scene.name == "MainScene" && _showResultOnLoad) // 미니게임 종료 후, 메인 씬 로딩
        {
            player = FindObjectOfType<PlayerController>();
            uiManager = FindObjectOfType<UIManager>();
            miniGameButton = FindObjectOfType<MiniGameButtonController>();

            player.gameObject.SetActive(true);
            uiManager.ShowResultPanel(); // 미니게임 종료 후, 결과 패널을 보여준다

            _showResultOnLoad = false; // 결과 표시 후, 플래그 초기화
        }
    }
}
