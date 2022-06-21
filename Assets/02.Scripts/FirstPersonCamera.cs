using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
public class FirstPersonCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    
    private float xRotation;
    private float yRotation;
    private PlayerWeapon _playerWeapon;
    private Weapon _currentWeapon 
    {
        get => _playerWeapon._currentWeapon == null ? null : _playerWeapon._currentWeapon;  
    }
    #region 총기반동 구현 변수
    private float recoilX { get => _currentWeapon == null ? 0 :_currentWeapon._weaponData.recoilX; }
    private float recoilY { get => _currentWeapon == null ? 0 : _currentWeapon._weaponData.recoilY; }
    private float recoilZ { get => _currentWeapon == null ? 0 : _currentWeapon._weaponData.recoilZ; }
    private float snappiness { get => _currentWeapon == null ? 0 : _currentWeapon._weaponData.snappiness; }
    private float returnSpeed { get => _currentWeapon == null ? 0 : _currentWeapon._weaponData.returnSpeed; }

    private Vector3 currentRotation;
    private Vector3 targetRotation;
    #endregion
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerWeapon = GameObject.Find("PlayerWeapon").GetComponent<PlayerWeapon>();
    }
    private void Update()
    {
        FollowCamera();
    }
    private void FollowCamera()
    {

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation += currentRotation.x;
        xRotation = Mathf.Clamp(xRotation, -45f, 80);
        yRotation += currentRotation.y;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, currentRotation.z);


        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
