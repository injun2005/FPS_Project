using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Enemy : PoolableMono, IHittable
{
    private NavMeshAgent _agent = null;
    public EnemyDataSO _enemyData;
    private CapsuleCollider _enemyCollider;
    private EnemyAnimation _enemyAnimator;
    private EnemyAttack _enemyAttack;

    public float rotateSpeed = 10f;
    [field: SerializeField] private UnityEvent OnHitFeedback;
    [field: SerializeField] private UnityEvent OnDieFeedback;

    public int Hp { get; private set; }
    public bool isDead = false;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyCollider = GetComponent<CapsuleCollider>();
        _enemyAnimator = GetComponent<EnemyAnimation>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }
    private void Start()
    {
        Hp = _enemyData.maxHp;
    }
    public void Move(Vector3 dir)
    {
        if (isDead) return;
        _agent.Move(dir * Time.deltaTime);
        _enemyAnimator.SetMoveParams(dir.z, dir.x);
    }
    public void Rotate(Vector3 dir)
    {
        if (isDead) return;
        transform.rotation = Quaternion.LookRotation(dir * Time.deltaTime * rotateSpeed);
    }
    public void PerformAttack()
    {
        if (isDead) return;
        _enemyAttack.Attack(_enemyData.damage);
    }
    public override void Reset()
    {
        Hp = _enemyData.maxHp;
        isDead = false;
        _enemyCollider.enabled = true;
    }

    public void Die()
    {
        isDead = true;
        _enemyCollider.enabled = false;
        OnDieFeedback?.Invoke();
    }
    public void PushEnemy()
    {
        PoolManager.Instance.Push(this);
    }
    public void GetHit(int damage, Vector3 hitPos)
    {
        if (isDead) return;
        Hp -= damage;
        OnHitFeedback?.Invoke();

        if (Hp <= 0)
        {
            Die();
            GameMGR.Instance.RemainMonster();
        }
    }
}
