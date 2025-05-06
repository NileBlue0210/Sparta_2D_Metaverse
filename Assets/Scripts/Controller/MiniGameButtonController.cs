using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameButtonController : MonoBehaviour
{
    Animator animator;

    private bool isClicked = false;
    private bool isPlayerClose = false;

    public bool IsClicked
    {
        get { return isClicked; }
        set { 
            isClicked = value;

            animator.SetBool("IsClicked", isClicked);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsMiniGameActive)
            return;

        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        IsClicked = true;

        GameManager.Instance.StartMiniGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}
