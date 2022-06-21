using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        _aiActionData.attack = false;
        Vector3 dir = _enemyAIBrain.targetTrm.position - transform.position;
        _aiMovementData.direction = dir;
        _aiMovementData.pointOfInterest = _aiMovementData.direction;
        _enemyAIBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
