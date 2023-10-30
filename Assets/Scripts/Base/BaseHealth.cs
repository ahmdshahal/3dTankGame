using System;
using General;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Base
{
    public class BaseHealth : MonoBehaviour
    {
        [SerializeField] private GameManager GameManagerScript;
        [SerializeField] private ParticleSystem explosionEffect; // Efek ledakan
        [SerializeField] private Image healthBar; // Bar kesehatan base
        [SerializeField] private float baseHealth = 500; // Nilai kesehatan base
        [SerializeField] private string playerWin = "Player 2"; // Jika base ini hancur maka, player yang menang adalah

        private MeshRenderer m_MeshRenderer;
        private Color m_FullHealthColor = Color.green; // Warna bar jika kesehatan penuh
        private Color m_ZeroHealthColor = Color.red; // Warna bar jika kesehatan habis
        private float m_CurrentHealth; // Nilai kesehatan saat ini

        private void Awake()
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            m_CurrentHealth = baseHealth; // Reset kesehatan base
            SetHealthUI();
        }

        public void TakeDamage(float amount)
        {
            // Mengurangi nilai kesehatan
            m_CurrentHealth -= amount;
            SetHealthUI();

            // Jika nilai kesehatan habis, maka base akan hancur dan lawan menang
            if (m_CurrentHealth <= 0)
            {
                m_MeshRenderer.enabled = false;
                explosionEffect.Play();
                GameManagerScript.Win(playerWin);
            }
        }

        private void SetHealthUI()
        {
            // Set nilai health bar dengan nilai kesehatan saat ini
            healthBar.fillAmount = m_CurrentHealth / baseHealth;

            // Mengganti warna health bar sesuai dengan nilai kesehatan saat ini
            healthBar.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / baseHealth);
        }
    }
}
