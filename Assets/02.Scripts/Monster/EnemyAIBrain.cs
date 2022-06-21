using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyAIBrain : MonoBehaviour
{
    public Transform targetTrm { get; private set; }
    [field: SerializeField] private UnityEvent<Vector3> OnMovePress;
    [field: SerializeField] private UnityEvent<Vector3> OnRotatePress;
    [field: SerializeField] private UnityEvent OnAttackPress;

    [SerializeField] private AIState _currentState;
   
    private AIActionData _actionData;
    public AIActionData ActionData => _actionData;
    private void Awake()
    {
        targetTrm = GameObject.Find("Player").transform;
        _actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetAttackState(bool state)
    {
        _actionData.attack = state;
    }
    public void ChangeState(AIState state)
    {
        _currentState = state;
    }
    public void Attack()
    {
        OnAttackPress?.Invoke();
    }
    public void Move(Vector3 moveDir, Vector3 targetDir)
    {
        OnMovePress?.Invoke(moveDir);
        OnRotatePress?.Invoke(targetDir);
    }

    private void Update()
    {
        if(targetTrm == null)
        {
            OnMovePress?.Invoke(Vector3.zero);
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}
