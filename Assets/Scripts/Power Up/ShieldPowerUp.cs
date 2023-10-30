using System;
using System.Collections;
using Tank;
using UnityEngine;

namespace Power_Up
{
    public class ShieldPowerUp : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tank"))
            {
                if (other.TryGetComponent(out TankHealth tankHealth))
                {
                    Activate(tankHealth);
                    ReturnToPool();
                }
            }
        }

        private void Activate(TankHealth tankHealth)
        {
            tankHealth.ActivateShield();
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
