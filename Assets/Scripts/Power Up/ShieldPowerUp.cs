using System;
using System.Collections;
using Tank;
using UnityEngine;

namespace Power_Up
{
    public class ShieldPowerUp : MonoBehaviour
    {
        [SerializeField] private float shieldDuration;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tank"))
            {
                if (other.TryGetComponent(out TankHealth tankHealth))
                {
                    Activate(tankHealth);
                }
            }
        }

        private void Activate(TankHealth tankHealth)
        {
            tankHealth.ActivateShield();
            StartCoroutine(Deactivate(tankHealth));
        }

        private IEnumerator Deactivate(TankHealth tankHealth)
        {
            yield return new WaitForSeconds(shieldDuration);
            
            tankHealth.DeactivateShield();
        }
    }
}
