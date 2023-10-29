using Tank;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private TankInput m_TankInput;
    private TankInput.InGameActions m_InGameActions;
    
    private TankController m_TankControllerScript;
    private TankShooter m_TankShooterScript;

    private void Awake()
    {
        m_TankInput = new TankInput();
        m_InGameActions = m_TankInput.InGame;

        m_TankControllerScript = GetComponent<TankController>();
        m_TankShooterScript = GetComponent<TankShooter>();
        
        m_InGameActions.Rotation.performed += ctx => m_TankControllerScript.RotateTank(m_InGameActions.Rotation.ReadValue<float>());
        m_InGameActions.Fire.performed += ctx => m_TankShooterScript.FireMissile();
    }

    private void Update()
    {
        m_TankControllerScript.TankMovement(m_InGameActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        m_TankControllerScript.AimTurret(m_InGameActions.Aiming.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        m_InGameActions.Enable();
    }

    private void OnDisable()
    {
        m_InGameActions.Disable();
    }
}
