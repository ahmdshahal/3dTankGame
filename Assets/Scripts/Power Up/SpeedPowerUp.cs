using System.Collections;
using Tank;
using UnityEngine;

namespace Power_Up
{
    public class SpeedPowerUp : MonoBehaviour
    {
        [SerializeField] private float speedDuration;

        private float m_SpeedMultiplier = 2f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tank"))
            {
                if (other.TryGetComponent(out TankController tankController))
                {
                    Activate(tankController);
                }
            }
        }

        private void Activate(TankController tankController)
        {
            tankController.FasterTankSpeed(m_SpeedMultiplier);

            StartCoroutine(Deactivate(tankController));
        }

        private IEnumerator Deactivate(TankController tankController)
        {
            yield return new WaitForSeconds(speedDuration);
            
            tankController.NormalTankSpeed();
        }
    }
}
