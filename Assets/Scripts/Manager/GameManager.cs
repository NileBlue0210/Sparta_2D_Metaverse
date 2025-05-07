using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// to do: �̴ϰ����� �����ϴ� �Ŵ����� ���� ����� ���� �´� �� ����
public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }

    private bool isMiniGameActive = false;
    public bool IsMiniGameActive { get { return isMiniGameActive; } }

    public MiniGameButtonController miniGameButton { get; private set; }
    public MiniGameManager miniGameManager { get; private set; }
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
        miniGameButton = FindObjectOfType<MiniGameButtonController>();
    }

    public void ShowMiniGame()
    {
        if (isMiniGameActive)
            return;

        isMiniGameActive = true;
        player.gameObject.SetActive(false); // �̴ϰ��� ��, �÷��̾� ������Ʈ�� �����ϴ� ���� ���� ���� ��Ȱ��ȭ

        // SceneManager.LoadScene("FlappingMiniGameScene", LoadSceneMode.Additive); // to do: ���� Additive�ɼ��� ����� �˾� â���� �����ϴ� �������� �����غ� ��
        SceneManager.LoadScene("FlappingMiniGameScene");
    }

    public void HideMiniGame()
    {
        if (!isMiniGameActive)
            return;

        SceneManager.LoadScene("MainScene");

        isMiniGameActive = false;
        // player.gameObject.SetActive(true); // �̴ϰ��� ���� ��, ��Ȱ��ȭ �Ǿ��ִ� �÷��̾� ������Ʈ �ٽ� Ȱ��ȭ
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �̴ϰ��� �� �ε� ��, �̴ϰ����� �Ŵ����� Ž�� �� �̴ϰ��� ���� �Լ��� ȣ���Ѵ�
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

        if (scene.name == "MainScene")
        {
            player = FindObjectOfType<PlayerController>();
            miniGameButton = FindObjectOfType<MiniGameButtonController>();
            player.gameObject.SetActive(true);
        }
    }
}
