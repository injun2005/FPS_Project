using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEffect : PoolableMono
{
    public AudioSource _audioSource;
    public float lifeTime = 1f;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public override void Reset()
    {
        transform.localRotation = Quaternion.identity;   
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        if(_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }
        Invoke("DestroyAfterAnimation", lifeTime);
    }

    public void DestroyAfterAnimation() {
        PoolManager.Instance.Push(this);
    }
}
