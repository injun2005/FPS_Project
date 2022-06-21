using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public WeaponSO _weaponData;
    public Transform firePos;
    public UnityEvent OnShootFeedback;
    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    private Player _player;
    public ParticleSystem _muzzleFlash;
    public float spreadAmount = 1f;
    private bool isShootingDelay = false;
    private bool isShoot = false;
    private FirstPersonCamera _recoilCamera;
    [SerializeField] protected int _ammo;
    public int Ammo
    {
        get
        {
            return _weaponData._infitiniteAmmo ? -1 : _ammo;
        }
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
            OnChangedAmmo?.Invoke(_ammo);
        }
    }
    public bool AmmoFull { get => Ammo == _weaponData.ammoCapacity || _weaponData._infitiniteAmmo; }
    public int EmptyBulletCnt { get => _weaponData.ammoCapacity - _ammo; }

    public UnityEvent<int> OnChangedAmmo;
    private void Awake()
    {
        _ammo = _weaponData.ammoCapacity;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _recoilCamera = GameObject.Find("Main Camera").GetComponent<FirstPersonCamera>();
    }
    public void ResetWeapon()
    {
        isShoot = false;
        isShootingDelay = false;
    }
    private void Update()
    {
        UseWeapon();
    }
    public virtual void UseWeapon()
    {
        if (isShoot && !isShootingDelay)
        {
            if (Ammo > 0 || _weaponData._infitiniteAmmo)
            {
                for (int i = 0; i < _weaponData.GetBulletCountToSpawn(); i++)
                {
                    if (!_weaponData._infitiniteAmmo)
                    {
                        Ammo--;
                        if (Ammo <= 0)
                            break;
                    }
                    ShootBullet(); // 차후 구현
                }
                _recoilCamera.RecoilFire();
            }
            else
            {
                isShoot = false;
                return;
            }
            FinishShoot();
        }

    }
    public void TryShooting()
    {
        isShoot = true;
    }
    public void StopShooting()
    {
        isShoot = false;
    }
    public virtual void ShootBullet()
    {
        OnShootFeedback?.Invoke();
        SpawnMuzzleEffect();
        ShotBulletRay();
    }

    private void SpawnMuzzleEffect()
    {
        if (_muzzleFlash.gameObject.activeSelf == false)
            _muzzleFlash.gameObject.SetActive(true);
        _muzzleFlash.Play();
    }

    public IEnumerator ShootCoroutine()
    {
        isShootingDelay = true;
        yield return new WaitForSeconds(_weaponData._attackDelay);
        isShootingDelay = false;
    }

    protected virtual void ShotBulletRay()
    {
        RaycastHit hit;
        Vector3 shootDirection = Camera.main.transform.forward;
        shootDirection.x += Random.Range(-spreadAmount*1.5f, spreadAmount*1.5f) ;
        shootDirection.y += Random.Range(-spreadAmount/2, spreadAmount/2);
        Debug.DrawRay(Camera.main.transform.position, shootDirection * _weaponData._attackRange, Color.red);
        if (Physics.Raycast(Camera.main.transform.position, shootDirection, out hit, _weaponData._attackRange))
        {
            IHittable hittable = hit.transform.GetComponent<IHittable>();
            hittable?.GetHit(damage: _weaponData._damage, hitPos: hit.point);

            if(hittable == null)
            {
                PoolEffect effect = PoolManager.Instance.Pop(_weaponData._hitObstacleEffectPrefab.name) as PoolEffect;
                effect.SetPositionAndRotation(hit.point, Quaternion.LookRotation(-hit.transform.forward));
            }
        }
    }
    
    public void FinishShoot()
    {
        StartCoroutine(ShootCoroutine());
        if(!_weaponData._isSeries)
        {
            isShoot = false;
        }
    }

}
