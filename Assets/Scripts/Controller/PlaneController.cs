using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    public bool godMode = false;

    private float deathCooldown = 0f;
    private bool isFlap = false;

    GameManager gameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance; // MainScene�� GameManager�� �����´�
        animator = GetComponentInChildren<Animator>();  // �ڽ� ������Ʈ Model�� �ִ� �ִϸ����͸� �����´�
        _rigidbody = GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("not founded plane animator");

        if (_rigidbody == null)
            Debug.LogError("not founded plane rigidbody");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.MiniGameRestart();
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;

        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity; // ���� Vector3 velocity���� _rigidbody.velocity�� �Ҵ��� ���� ���� �����̸�, ������ rigidbody�� �Ҵ��� ���� �ƴϱ� ������ ��ȭ�� ������ ���� ������� �ֱ� ����

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);  // ���� ����, rigidbody�� y�� �ӵ��� ���������� y�� �ӵ��� �ö󰡸� ������ 90������ �ö󰡰� �������� -90������ ������
        transform.rotation = Quaternion.Euler(0, 0, angle); // Quaternion.Euler�� ������ �����ϴ� �޼ҵ�, z�� ȸ���� angle�� �����ϰ� x��� z���� 0���� ������
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;

        if (isDead) return;

        animator.SetBool("IsDie", true);

        isDead = true;
        deathCooldown = 1f;

        gameManager.MiniGameOver();
    }
}
