using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSword : MonoBehaviour
{
    private BoxCollider _attakCollider;
    private EnemyAIBrain _enemyBrain;
    private Enemy _enemy;

    private void Awake()
    {
        _attakCollider = GetComponent<BoxCollider>();
        _enemyBrain = transform.root.GetComponent<EnemyAIBrain>();
        _enemy = transform.root.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_enemyBrain.ActionData.attack) return;
        if(other.CompareTag("PLAYER"))
        {
            IHittable hittable = _enemyBrain.targetTrm.GetComponent<IHittable>();
            hittable.GetHit(_enemy._enemyData.damage, other.transform.position);
            _enemyBrain.SetAttackState(false);
        }    
    }


}
