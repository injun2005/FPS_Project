using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using TMPro;
using System.Linq;
public class GameMGR : MonoBehaviour
{
    [SerializeField] private PoolingDataSO poolList;
    public TMP_Text remainMonsterText;
    private int maxMonster = 0;
    private Player _player;
    public List<Enemy> enemyList = new List<Enemy>();

    public UnityEvent OnGameOver;
    public UnityEvent OnGameClear;
    private bool _isGameClear =false;
    private bool _isGameOver = false;
    public bool IsGameOver
    {
        get
        {
            return _isGameOver;
        }
        set
        {
            _isGameOver = value;
        }
    }
    private void Update()
    {
        if(_isGameOver)
        {
            OnGameOver?.Invoke();
        }
        if(_isGameClear)
        {
            OnGameClear?.Invoke();
        }
    }
    private static GameMGR _instance;
    public static GameMGR Instance
    {
        get 
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameMGR>();
                if(_instance == null)
                {
                    GameObject container = new GameObject("GameMGR");
                    _instance = container.AddComponent<GameMGR>();
                }
            }
           return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        //DontDestroyOnLoad(gameObject);
        new PoolManager(transform);
        CreatePool();
        enemyList = FindObjectsOfType<Enemy>().ToList();
        maxMonster = enemyList.Count;
        _player = GameObject.Find("Player").GetComponent<Player>();
        RemainMonster();
    }
    public void CreatePool()
    {
        foreach(PoolData data in poolList.list)
        {
            PoolManager.Instance.CreatePool(data.prefab, data.poolCnt);
        }
    }

    public void RemainMonster()
    {
        maxMonster --;
        remainMonsterText.text = $"{maxMonster}";
        if(maxMonster <= 0)
        {
            _isGameClear = true;
        }
    }
}
