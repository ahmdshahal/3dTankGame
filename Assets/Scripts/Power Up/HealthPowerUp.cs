using System;
using Tank;
using UnityEngine;

namespace Power_Up
{
    public class HealthPowerUp : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 10; // Waktu hodup power up setelah aktif
        
        private void OnTriggerEnter(Collider other)
        {
            // Kondisi jika diambil oleh Tank
            if (other.CompareTag("Tank"))
            {
                if (other.TryGetComponent(out TankHealth tankHealth))
                {
                    Activate(tankHealth);
                    CancelInvoke(); // Membatalkan waktu hidup, karena sudah diambil oleh tank
                    ReturnToPool();
                }
            }
        }

        private void Activate(TankHealth tankHealth)
        {
            // Mereset nilai kesehatan Tank
            tankHealth.ResetTankHealth();
        }

        private void OnEnable()
        {
            // Set waktu hodup
            Invoke("ReturnToPool", lifeTime);
        }

        private void ReturnToPool()
        {
            // Mengembalikan ke pool
            gameObject.SetActive(false);
        }
    }
}
