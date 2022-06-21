using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerWeapon : MonoBehaviour
{
    private List<Weapon> _weaponList = new List<Weapon>();
    private AudioSource _audioSource;
    private Player _player;
    private int _currentWeaponIndex = 0;
    private bool _isChangeWeapon = false;
    public Weapon _currentWeapon = null;
    [SerializeField]
    public ReloadGaugeUI _reloadUI;

    [field:SerializeField]
    public UnityEvent<int, int> OnChangedTotalAmmo { get; set; }

    [SerializeField] private int _maxTotalAmmo = 1000;
    [SerializeField] private int _totalAmmo = 200;
    public UnityEvent<List<Weapon>, int> UpdateWeaponUI;
    public UnityEvent<bool, Action> ChangeWeaponUI;
    public bool AmmoFull { get => _totalAmmo == _maxTotalAmmo; } 

    public int TotalAmmo
    {
        get => _totalAmmo;
        set
        {
            _totalAmmo = Mathf.Clamp(value, 0, _maxTotalAmmo);
            OnChangedTotalAmmo?.Invoke(_totalAmmo, _maxTotalAmmo);
        }
    }

    private bool _isReloading = false;
    public bool IsReloading { get => _isReloading; }

    //private void AssignWweapon()
    //{
        
    //}나중에 무기 줍기 추가시 넣을 함수
    private void Awake()
    {
        _player = transform.GetComponentInParent<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Weapon[] weapons = GetComponentsInChildren<Weapon>();

        for(int idx = 0; idx < _player.maxWeapons; idx++)
        {
            if(weapons.Length <= idx)
            {
                _weaponList.Add(null);
            }
            else
            {
                _weaponList.Add(weapons[idx]);
                weapons[idx].gameObject.SetActive(false);
            }
        }
        if(_weaponList.Count > 0)
        {
            _currentWeapon = _weaponList[0];
            _currentWeapon.gameObject.SetActive(true);
        }
        UpdateWeaponUI?.Invoke(_weaponList, _currentWeaponIndex);
    }
    public void Shoot()
    {
        if (_isReloading)
        {
            return;
        }
        if(_currentWeapon != null)
        {
            _currentWeapon.TryShooting();
        }
    }
    public void PlayShootSFX()
    {
        PlayClip(_currentWeapon.shootSFX);
    }
    public void StopShooting()
    {
        if(_currentWeapon != null)
        {
            _currentWeapon.StopShooting();
        }
    }
    public void ChangeToNextWeapon(bool isPrev)
    {
        if (_isReloading || _weaponList.Count <= 1 || _isChangeWeapon == true)
        {
            return;
        }
        _isChangeWeapon = true;
        _currentWeapon?.gameObject.SetActive(false);
        ChangeWeaponUI?.Invoke(isPrev, () =>
        {
            int nextIdx = 0;
            if (isPrev)
            {
                nextIdx = _currentWeaponIndex - 1 < 0 ? _weaponList.Count - 1 : _currentWeaponIndex - 1;
            }
            else
            {
                nextIdx = (_currentWeaponIndex + 1) % _weaponList.Count;
            }
            ChangeWeapon(_weaponList[nextIdx]);
            _currentWeaponIndex = nextIdx;

            _isChangeWeapon = false;
        });
    }
    public void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        if(weapon != null)
        {
            weapon.gameObject.SetActive(true);
            weapon._muzzleFlash.gameObject.SetActive(false);
            weapon.ResetWeapon();
        }
    }
    public void ReloadGun()
    {
        if(_currentWeapon != null && !_isReloading && _totalAmmo > 0 && !_currentWeapon.AmmoFull)
        {
            _isReloading = true;
            _currentWeapon.StopShooting();

            PlayClip(_currentWeapon.reloadSFX);
            StartCoroutine(RelaodCoroutine());
        }
    }
    private void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    private IEnumerator RelaodCoroutine()
    {
        _reloadUI.gameObject.SetActive(true);
        float time = 0;
        while (time <= _currentWeapon._weaponData._reloadTime)
        {
            _reloadUI.ReloadGaugeNormal(time/_currentWeapon._weaponData._reloadTime);
            time += Time.deltaTime;
            yield return null;
        }
        _reloadUI.gameObject.SetActive(false);

        int reloadAmmo = Mathf.Min(TotalAmmo, _currentWeapon.EmptyBulletCnt);
        TotalAmmo -= reloadAmmo;
        _currentWeapon.Ammo += reloadAmmo;

        _isReloading = false;
    }

  
}
