using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
        CancelInvoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            if (other.TryGetComponent(out TankShooter tankShooter))
            {
                tankShooter.ReloadAmmo();
                ReturnToPool();
            }
        }
    }
}
