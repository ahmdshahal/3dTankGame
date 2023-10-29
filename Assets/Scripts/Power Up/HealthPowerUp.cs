using Tank;
using UnityEngine;

namespace Power_Up
{
    public class HealthPowerUp : MonoBehaviour
    {
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
            tankHealth.ResetTankHealth();
        }
    }
}
