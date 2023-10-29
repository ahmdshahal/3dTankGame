using System.Collections;
using Tank;
using UnityEngine;

namespace Power_Up
{
    public class SpeedPowerUp : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 10;

        private float m_SpeedUp = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Tank"))
            {
                if (other.TryGetComponent(out TankController tankController))
                {
                    Activate(tankController);
                    CancelInvoke();
                    ReturnToPool();
                }
            }
        }

        private void Activate(TankController tankController)
        {
            tankController.FasterTankSpeed(m_SpeedUp);
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
