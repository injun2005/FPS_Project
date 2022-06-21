using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCcontrol : MonoBehaviour
{
    public GameObject ExpPrefab;

    public Texture[] textures;
    [Header("STAT")]
    public int hitCount = 0;
    public float expForce = 1500;
    public float radius = 10f;
       
    private Rigidbody _rigidbody;

    private MeshRenderer _renderer;
    Collider[] colls = new Collider[10];
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = transform.Find("barrelsprite").GetComponent<MeshRenderer>();

        int idx = UnityEngine.Random.Range(0, textures.Length);
        _renderer.material.mainTexture = textures[idx];
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    private void ExpBarrel()
    {
        GameObject exp = Instantiate(ExpPrefab, transform.position, transform.rotation);

        Destroy(exp, 3f);

        //_rigidbody.mass = 1.0f;
        //_rigidbody.AddForce(Vector3.up * expForce);
        IndirectDamage(transform.position);
        Destroy(gameObject, 3f);
    }
   
    private void IndirectDamage(Vector3 pos)
    {
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);

        foreach (var coll in colls)
        {
            if (coll == null)
                continue;
            Rigidbody rb = coll.GetComponent<Rigidbody>();
            rb.mass = 1;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(expForce,pos, radius,1200f);
        }
    }
}
