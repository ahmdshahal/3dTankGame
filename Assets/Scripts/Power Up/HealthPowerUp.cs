using System;
using Tank;
using UnityEngine;

namespace Power_Up
{
    public class HealthPowerUp : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 10;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tank"))
            {
                if (other.TryGetComponent(out TankHealth tankHealth))
                {
                    Activate(tankHealth);
                    CancelInvoke();
                    ReturnToPool();
                }
            }
        }

        private void Activate(TankHealth tankHealth)
        {
            tankHealth.ResetTankHealth();
        }

        private void OnEnable()
        {
            Invoke("ReturnToPool", lifeTime);
        }

        private void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
