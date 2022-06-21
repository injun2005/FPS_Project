using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector3.zero;

        _enemyAIBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
