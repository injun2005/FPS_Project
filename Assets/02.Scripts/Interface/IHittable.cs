using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public void GetHit(int damage,Vector3 hitPos);
}
