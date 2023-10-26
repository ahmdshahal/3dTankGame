using System;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5; //Kecepatan bergerak
    [SerializeField] private float rotationSpeed = 25; //Kecepatan putaran meriam
    [SerializeField] private Transform turretTransform; //Transform dari meriam tank

    private float m_CurrentMoveSpeed;

    private void Start()
    {
        NormalTankSpeed();
    }

    public void TankMovement(Vector2 input)
    {
        // Mengatur pergerakan tank
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        transform.Translate(moveDirection * (m_CurrentMoveSpeed * Time.deltaTime));
    }

    public void RotateTank(float input)
    {
        // Mengatur rotasi tank
        transform.Rotate(0, input * 90, 0);
    }

    public void AimTurret(Vector2 input)
    {
        //Mengatur rotasi meriam
        float mouseX = input.x;
        turretTransform.Rotate( Vector3.up * (mouseX * rotationSpeed * Time.deltaTime), Space.World);
    }

    public void OnDeath()
    {
        m_CurrentMoveSpeed = 0;
    }

    public void NormalTankSpeed()
    {
        m_CurrentMoveSpeed = moveSpeed;
    }
}
