using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to do: ī�޶� �̵� ���� ���� ����
public class MainCameraController : MonoBehaviour
{
    public Transform target;

    float offsetX;
    float offsetY;

    void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x; // ī�޶�� Ÿ���� ������ x�� �Ÿ�
        offsetY = transform.position.y - target.position.y; // ī�޶�� Ÿ���� ������ y�� �Ÿ�
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
