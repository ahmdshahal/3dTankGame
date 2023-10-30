using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tank
{
    public class TankHealth : MonoBehaviour
    {
        public GameObject tankShield;
        public float shieldDuration = 5;
        
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private GameObject tankRenderers;
        [SerializeField] private Image healthBar;
        [SerializeField] private float tankHealth = 200;
        [SerializeField] private float respawnTime = 3;

        private TankMovement m_TankMovement;
        private MeshRenderer m_MeshRenderer;
        private Vector3 m_OriginPosition;
        private Quaternion m_OriginRotation;
        private Color m_FullHealthColor = Color.green;
        private Color m_ZeroHealthColor = Color.red;
        private float m_CurrentHealth;
        private bool m_IsDead;

        private void Awake()
        {
            m_TankMovement = GetComponent<TankMovement>();
        }

        private void Start()
        {
            //Menyimpan posisi awal tank
            m_OriginPosition = transform.position;
            m_OriginRotation = transform.rotation;
        }

        private void OnEnable()
        {
            ResetTankHealth();
            SetHealthUI();
            m_IsDead = false;
            tankRenderers.SetActive(true);
        }

        public void ResetTankHealth()
        {
            //Mereset health saat ini
            m_CurrentHealth = tankHealth;
        }

        public void TakeDamage(float amount)
        {
            //Mengurangi health saat ini dengan amount
            m_CurrentHealth -= amount;
            SetHealthUI();

            //Jika health saat ini kurang dari 0 dan tidak sedang dead maka tank akan mati
            if (m_CurrentHealth <= 0 & !m_IsDead)
            {
                StartCoroutine(OnDeath());
            }
        }

        private IEnumerator OnDeath()
        {
            m_IsDead = true;

            //Aktifkan efek ledakan
            explosionEffect.Play();
        
            tankRenderers.SetActive(false); //Menonaktifkan tank
            m_TankMovement.StopTankSpeed(); //Mengehentikan gerakan tank
        
            //IENumerator akan berhenti sesuai durasi dari efek ledakan
            ParticleSystem.MainModule mainModule = explosionEffect.main;
            yield return new WaitForSeconds(mainModule.duration);

            //Memulai coroutine untuk merespawn tank
            StartCoroutine(RespawnTank());
        }

        private IEnumerator RespawnTank()
        {
            transform.position = m_OriginPosition; //Mengembalikan posisi tank ke posisi awal
            transform.rotation = m_OriginRotation;
        
            yield return new WaitForSeconds(respawnTime);
        
            tankRenderers.SetActive(true); //Setelah respawn time, mengaktifkan tank kembali
            m_TankMovement.NormalTankSpeed(); //Mengembalikan kecepatan tank
            
            ResetTankHealth();
            m_IsDead = false;
        }

        public void ActivateShield()
        {
            tankShield.SetActive(true);
            
            Invoke("DeactivateShield", shieldDuration);
        }

        private void DeactivateShield()
        {
            tankShield.SetActive(false);
        }

        private void SetHealthUI()
        {
            healthBar.fillAmount = m_CurrentHealth / tankHealth;

            healthBar.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / tankHealth);
        }
    }
}
