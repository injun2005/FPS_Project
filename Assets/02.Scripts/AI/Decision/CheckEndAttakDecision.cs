using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndAttakDecision : AIDecision
{
    public override bool MakeDecision()
    {
        return !_aiActionData.attack;
    }

  
}
