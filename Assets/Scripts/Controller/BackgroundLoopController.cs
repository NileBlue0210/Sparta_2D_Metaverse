using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    public int numBgCount = 5;  // 반복할 배경 갯수
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        ObstacleController[] obstacles = GameObject.FindObjectsOfType<ObstacleController>();    // findobjectoftype는 무거운 코드이므로 awake나 start등에서 한 번 정도만 작동하도록 하는 것이 좋다.
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        // 배치된 모든 장애물들의 시작 위치를 랜덤하게 설정
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObstacleController obstacle = collision.GetComponent<ObstacleController>();

        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);

            return; // 장애물일 경우, 아래 배경 복사 로직은 실행하지 않는다.
        }

        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; // BackGround의 콜라이더는 BoxCollider2D로 설정되어 모든 콜라이더를 포괄하는 Collider2D로는 사이즈값을 가져올 수 없다. 그러므로 BoxCollider2D로 형변환을 해줄 필요가 있다.
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos; // 트리거와 충돌한 배경의 x값 위치를 반복되는 배경의 가장 끝 줄에 가져다놓는다.
        }
    }
}
