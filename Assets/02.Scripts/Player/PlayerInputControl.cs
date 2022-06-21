using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputControl : MonoBehaviour
{
    private AudioSource _audioSource;

    private bool _fireButtonDown = false;

    [field:SerializeField]private UnityEvent OnShoot;
    [field:SerializeField]private UnityEvent OffShoot;
    [field:SerializeField]private UnityEvent<bool> OnChangedNextWeapon;
    [field:SerializeField]private UnityEvent OnReloadKeyPress;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Fire();
        ChangedNextWeapon();
        InputReloadButton();
    }
    private void ChangedNextWeapon()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
           OnChangedNextWeapon?.Invoke(true);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OnChangedNextWeapon?.Invoke(false);
        }
    }
    private void Fire()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (!_fireButtonDown)
            {
                _fireButtonDown = true;
                OnShoot?.Invoke();
            }
        }
        else
        {
            if (_fireButtonDown)
            {
                _fireButtonDown = false;
                OffShoot?.Invoke();
            }
        }
    }
    private void InputReloadButton()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnReloadKeyPress?.Invoke();
        }
    }
}
