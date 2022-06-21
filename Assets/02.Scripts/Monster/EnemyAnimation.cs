using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    protected EnemyAIBrain _enemyAiBrain;
    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _speedhHash = Animator.StringToHash("speedh");
    protected readonly int _speedvHash = Animator.StringToHash("speedv");
    protected readonly int _deathHash = Animator.StringToHash("Death");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyAiBrain = GetComponent<EnemyAIBrain>();
    }
    public void PlayAttackAnim()
    {
        _animator.SetTrigger(_attackHash);
    }
    public void EndAttackAnimation()
    {
        _enemyAiBrain.SetAttackState(false);
    }
    public void SetMoveParams(float v, float h)
    {
        _animator.SetFloat(_speedhHash, h);
        _animator.SetFloat(_speedvHash, v);
    }
    public void PlayDeathAnim()
    {
        _animator.SetTrigger(_deathHash);
    }
}
