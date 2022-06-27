using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Player : MonoBehaviour, IHittable
{
    public float moveSpeed = 3f;

    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool isGround;
    private float horizontal;
    private float vertical;
    private Vector3 moveDir;
    private Rigidbody rb;
    private Animator _animator;
    private readonly float iniHp = 100.0f;
    private Image hpBar;
    public float currHp;
    public int maxWeapons;
    public UnityEvent OnHitFeedback;
    IEnumerator Start()
    {
        currHp = iniHp;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
        hpBar = GameObject.FindGameObjectWithTag("HPBAR").GetComponent<Image>();
        rb = GetComponent<Rigidbody>();
        DisPlayHP();
        yield return new WaitForSeconds(0.3f);
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        MyInput();
        isGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (isGround)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void Move()
    {
        moveDir = transform.forward * vertical + transform.right * horizontal;

        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }
    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void PlayerDie()
    {
        GameMGR.Instance.GameOver(); 
    }
    public void DisPlayHP()
    {
        hpBar.fillAmount = currHp / iniHp;
    }

    public void GetHit(int damage, Vector3 hitPos)
    {
        currHp -= damage;
        OnHitFeedback?.Invoke();
        if (currHp <= 0)
        {
            PlayerDie();
        }
        DisPlayHP();
    }
}
