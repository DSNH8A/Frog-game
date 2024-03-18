using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Field field;
    [SerializeField]
    private GameObject right;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool canMove = false;
    public int lives = 3;
    private UIManager manager;
    private bool isDead = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0f, -3.75f, 0f);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        manager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        Movement();

        if (transform.position.x < -13)
        {
            transform.position = new Vector3(13.0f, transform.position.y, transform.position.z);   
        }

        if (transform.position.x > 13)
        {
            transform.position = new Vector3(-13.0f, transform.position.y, transform.position.z);
        }

        field.SetOrigin(this.transform.position);
        //field.SetAimDirection(transform.position);
    }

    private void Movement()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {    
            canMove = true;
            if (canMove == true )
            {
                animator.SetBool("MoveForward", true);
                rb.velocity = new Vector2(0, 5f);                
                StartCoroutine(MoveRoutine());   
            }

            canMove = false;
        }
        else
        {
            animator.SetBool("MoveForward", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {    
            canMove = true;
            if (canMove == true )
            {
                rb.velocity = new Vector2(-5f, 0);
                spriteRenderer.flipX = false;
                animator.SetBool("MoveLeft", true);
                StartCoroutine(MoveRoutine());
            }

            canMove = false;
        }
        else
        {
            animator.SetBool("MoveLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {    
            canMove = true;
            if (canMove == true )
            {
                rb.velocity = new Vector2(5f, 0);
                spriteRenderer.flipX = true;
                animator.SetBool("MoveRight", true);
                StartCoroutine(MoveRoutine());
            }

            canMove = false;
        }
        else
        {
            animator.SetBool("MoveRight", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {    
            canMove = true;
            if (canMove == true )
            {
                rb.velocity = new Vector2(0, -5f);
                animator.SetBool("BackWords", true);
                StartCoroutine(MoveRoutine());
            }

            canMove = false;
        }
        else
        {
            animator.SetBool("BackWords", false);
        }
    }
        

    IEnumerator MoveRoutine()
    {    
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(0, 0);
    }

    void Death()
    {
        if (lives < 1)
        {
            isDead  = true;
            Destroy(this.gameObject);
            manager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Car")
        {
            lives -= 1;
            manager.Damage(lives);
        }

        Death();
    }
}