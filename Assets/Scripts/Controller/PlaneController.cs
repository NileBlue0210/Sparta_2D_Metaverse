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
        gameManager = GameManager.Instance; // MainScene의 GameManager를 가져온다
        animator = GetComponentInChildren<Animator>();  // 자식 오브젝트 Model에 있는 애니메이터를 가져온다
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

        _rigidbody.velocity = velocity; // 위의 Vector3 velocity에서 _rigidbody.velocity를 할당한 것은 값의 복제이며, 실제로 rigidbody에 할당한 것이 아니기 때문에 변화한 에너지 값을 적용시켜 주기 위함

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);  // 각도 제한, rigidbody의 y축 속도를 감지함으로 y축 속도가 올라가면 각도가 90도까지 올라가고 내려가면 -90도까지 내려감
        transform.rotation = Quaternion.Euler(0, 0, angle); // Quaternion.Euler는 각도를 설정하는 메소드, z축 회전은 angle로 설정하고 x축과 z축은 0으로 설정함
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
