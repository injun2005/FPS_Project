using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PoolData
{
    public PoolableMono prefab;
    public int poolCnt;
}
[CreateAssetMenu(menuName = "SO/System/PoolingList")]
public class PoolingDataSO : ScriptableObject
{
    public List<PoolData> list;
}
