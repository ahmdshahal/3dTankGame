using System;
using System.Collections;
using System.Collections.Generic;
using Tank;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            if (other.TryGetComponent(out TankShooter tankShooter))
            {
                tankShooter.ReloadAmmo();
                CancelInvoke();
                ReturnToPool();
            }
        }
    }
}
