using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    public int numBgCount = 5;  // �ݺ��� ��� ����
    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        ObstacleController[] obstacles = GameObject.FindObjectsOfType<ObstacleController>();    // findobjectoftype�� ���ſ� �ڵ��̹Ƿ� awake�� start��� �� �� ������ �۵��ϵ��� �ϴ� ���� ����.
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        // ��ġ�� ��� ��ֹ����� ���� ��ġ�� �����ϰ� ����
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

            return; // ��ֹ��� ���, �Ʒ� ��� ���� ������ �������� �ʴ´�.
        }

        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; // BackGround�� �ݶ��̴��� BoxCollider2D�� �����Ǿ� ��� �ݶ��̴��� �����ϴ� Collider2D�δ� ������� ������ �� ����. �׷��Ƿ� BoxCollider2D�� ����ȯ�� ���� �ʿ䰡 �ִ�.
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos; // Ʈ���ſ� �浹�� ����� x�� ��ġ�� �ݺ��Ǵ� ����� ���� �� �ٿ� �����ٳ��´�.
        }
    }
}
