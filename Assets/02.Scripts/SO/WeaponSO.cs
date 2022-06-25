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
    public bool _isSeries; //���翩��
    public bool _infitiniteAmmo = false; //�Ѿ˹��� ����
    public float _attackRange = 20f; //���� ��Ÿ�
    [Range(0, 999)] public int ammoCapacity = 100; //��ź ����
    public float _reloadTime = 0.1f;
    public bool _multiBulletShoot = false;

    [Header("�ݵ��� ���� ��")]
    [Range(-1f,1f)]public float recoilX; //x��ǥ �ݵ�
    [Range(0,1f)]public float recoilY; //y��ǥ �ݵ�
    [Range(0,1f)]public float recoilZ; //z��ǥ �ݵ�
    public float snappiness; // �ݵ� ȸ����
    public float returnSpeed; // ȸ�� �ӵ�
    public int GetBulletCountToSpawn()
    {
        return _multiBulletShoot ? _bulletCount : 1;
    }
}
