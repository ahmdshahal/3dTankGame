using System;
using Tank;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    private TankInput m_TankInput;
    private TankInput.Player1Actions m_Player1Actions;
    private TankInput.Player2Actions m_Player2Actions;
    
    private TankMovement m_TankMovementScript;
    private TankShooter m_TankShooterScript;

    private void Awake()
    {
        m_TankInput = new TankInput();
        m_Player1Actions = m_TankInput.Player1;
        m_Player2Actions = m_TankInput.Player2;

        m_TankMovementScript = GetComponent<TankMovement>();
        m_TankShooterScript = GetComponent<TankShooter>();
    }

    private void Start()
    {
        // Mengecek input player agar sesuai dengan nomor player
        switch (playerNumber)
        {
            case 1:
                m_Player1Actions.Fire.performed += ctx => m_TankShooterScript.FireMissile();
                break;
            case 2:
                m_Player2Actions.Fire.performed += ctx => m_TankShooterScript.FireMissile();
                break;
        }
    }

    private void Update()
    {
        // Mengecek input player agar sesuai dengan nomor player
        switch (playerNumber)
        {
            case 1:
                m_TankMovementScript.MoveTank(m_Player1Actions.Movement.ReadValue<Vector2>());
                m_TankMovementScript.RotateTank(m_Player1Actions.Rotation.ReadValue<float>());
                break;
            case 2:
                m_TankMovementScript.MoveTank(m_Player2Actions.Movement.ReadValue<Vector2>());
                m_TankMovementScript.RotateTank(m_Player2Actions.Rotation.ReadValue<float>());
                break;
        }
    }
    
    private void LateUpdate()
    {
        // Kontrol turret dengan mousess
        //m_TankControllerScript.AimTurret(m_InGameActions.Aiming.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        m_Player1Actions.Enable();
        m_Player2Actions.Enable();
    }

    private void OnDisable()
    {
        m_Player1Actions.Disable();
        m_Player2Actions.Disable();
    }
}
