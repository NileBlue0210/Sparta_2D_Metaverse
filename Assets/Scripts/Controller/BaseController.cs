using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;

    public Vector2 MovementDirection { get { return movementDirection; } }

    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(movementDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    // to do: IMoveable �������̽��� MoveController Ŭ������ �̵� ����� �и��غ���
    protected virtual void Movement(Vector2 direction)
    {
        direction *= 5;

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    // ĳ���� ȸ��: �̵� ���⿡ ���� flip�� ���� Sprite ȸ��
    private void Rotate(Vector2 direction)
    {
        bool isLeft = direction.x < 0;

        characterRenderer.flipX = isLeft;
    }
}
