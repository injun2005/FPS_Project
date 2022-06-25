using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public int _bulletCount = 1;
    public GameObject _weaponPrefab;
    public GameObject _bulletPrefab;
    public GameObject _hitObstacleEffectPrefab;
    public Sprite _gunIcon;
    public int _damage;
    public float _attackDelay;
    public float _knockBackForce;
    public AudioClip _fireSFX;
    public bool _isSeries; //연사여부
    public bool _infitiniteAmmo = false; //총알무한 여부
    public float _attackRange = 20f; //공격 사거리
    [Range(0, 999)] public int ammoCapacity = 100; //총탄 개수
    public float _reloadTime = 0.1f;
    public bool _multiBulletShoot = false;

    [Header("반동에 대한 값")]
    [Range(-1f,1f)]public float recoilX; //x좌표 반동
    [Range(0,1f)]public float recoilY; //y좌표 반동
    [Range(0,1f)]public float recoilZ; //z좌표 반동
    public float snappiness; // 반동 회복력
    public float returnSpeed; // 회복 속도
    public int GetBulletCountToSpawn()
    {
        return _multiBulletShoot ? _bulletCount : 1;
    }
}
