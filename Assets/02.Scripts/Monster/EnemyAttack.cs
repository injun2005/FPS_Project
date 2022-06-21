using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyAIBrain _enemyAIBrain;
    protected Enemy _enemy;

    protected bool _waitBeforeNextAttack;

    protected float attackDelay => _enemy._enemyData.attackDelay;
    public bool WaitBeforeNextAttack { get => _waitBeforeNextAttack; }

    public UnityEvent AttackFeedback;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyAIBrain = GetComponent<EnemyAIBrain>();

        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(attackDelay);
        _waitBeforeNextAttack = false;
    }

    public void Reset()
    {
        StartCoroutine(WaitBeforeAttackCoroutine());
    }
    public Transform GetTarget()
    {
        return _enemyAIBrain.targetTrm;
    }
    public abstract void Attack(int damage);
}
