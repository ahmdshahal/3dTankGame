using System;
using General;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Base
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private Image healthBar;
        [SerializeField] private float baseHealth = 500;
        [SerializeField] private string playerWin = "Player 2";

        private MeshRenderer m_MeshRenderer;
        private Color m_FullHealthColor = Color.green;
        private Color m_ZeroHealthColor = Color.red;
        private float m_CurrentHealth;

        private void Awake()
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            m_CurrentHealth = baseHealth;
            SetHealthUI();
        }

        public void TakeDamage(float amount)
        {
            m_CurrentHealth -= amount;
            SetHealthUI();

            if (m_CurrentHealth <= 0)
            {
                m_MeshRenderer.enabled = false;
                explosionEffect.Play();
                gameManager.Win(playerWin);
            }
        }

        private void SetHealthUI()
        {
            healthBar.fillAmount = m_CurrentHealth / baseHealth;

            healthBar.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / baseHealth);
        }
    }
}
