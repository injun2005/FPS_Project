using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDecision : AIDecision
{
    public float _distance = 3f;

    public override bool MakeDecision()
    {
        float distance = Vector3.Distance(_enemyAIBrain.transform.position, _enemyAIBrain.targetTrm.position);

        return distance <= _distance;
    }
}
