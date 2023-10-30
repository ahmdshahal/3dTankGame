using UnityEngine;

namespace Tank
{
    public class TankMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5; //Kecepatan bergerak tank
        [SerializeField] private float turnSpeed = 100; //Kecepatan berbelok tank
        [SerializeField] private float speedUpDuration = 5; //Kecepatan berbelok tank

        private float m_CurrentMoveSpeed;

        private void Start()
        {
            NormalTankSpeed();
        }

        public void MoveTank(Vector2 input)
        {
            // Mengatur pergerakan tank
            Vector3 moveDirection = new Vector3(input.x, 0, input.y);
            transform.Translate(moveDirection * (m_CurrentMoveSpeed * Time.deltaTime));
        }

        public void RotateTank(float input)
        {
            // Mengatur rotasi tank
            transform.Rotate(0, input * (turnSpeed * Time.deltaTime), 0);
        }

        public void StopTankSpeed()
        {
            m_CurrentMoveSpeed = 0;
        }

        public void NormalTankSpeed()
        {
            m_CurrentMoveSpeed = moveSpeed;
        }

        public void FasterTankSpeed(float speedUp)
        {
            m_CurrentMoveSpeed = speedUp;
            
            Invoke("NormalTankSpeed", speedUpDuration);
        }
    }
}
