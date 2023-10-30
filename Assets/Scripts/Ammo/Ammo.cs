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
        // Set waktu hidup ammo
        Invoke("ReturnToPool", lifeTime);
    }

    private void ReturnToPool()
    {
        // Mengembalikan ammo ke pool
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jika ammo diambil oleh tank
        if (other.CompareTag("Tank"))
        {
            // Mengecek apakah ada komponen TankShooter pada Tank
            if (other.TryGetComponent(out TankShooter tankShooter))
            {
                //Menambah ammo pada tank tersebut
                tankShooter.ReloadAmmo();
                CancelInvoke(); // Membatalkan waktu hidup ammo, karena ammo sudah diambil oleh tank
                ReturnToPool();
            }
        }
    }
}
