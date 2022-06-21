using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{

    protected EnemyAIBrain _enemyAIBrain;
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;
    private void Awake()
    {
        _enemyAIBrain = transform.GetComponentInParent<EnemyAIBrain>();
        _aiActionData = transform.GetComponentInParent<AIActionData>();
        _aiMovementData = transform.GetComponentInParent<AIMovementData>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }
    public abstract bool MakeDecision();//transition�� ����ų������ �ƴ����� �����ؼ� bool�� ��ȯ
}