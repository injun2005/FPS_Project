using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public int maxHp = 50;
    public int damage = 5;
    public float attackDelay = 1f;
    //public float hitRange = 0;
    public PoolableMono MonsterPrebaf;
    public PoolableMono HitEffectPrebaf;
}
