using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to do: 카메라 이동 범위 제한 구현
public class MainCameraController : MonoBehaviour
{
    public Transform target;

    float offsetX;
    float offsetY;

    void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x; // 카메라와 타겟의 사이의 x축 거리
        offsetY = transform.position.y - target.position.y; // 카메라와 타겟의 사이의 y축 거리
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;

        pos.x = target.position.x + offsetX;
        pos.y = target.position.y + offsetY;

        transform.position = pos;
    }
}
