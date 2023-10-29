using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankHealth : MonoBehaviour
    {
        public GameObject tankShield;
        
        [SerializeField] private ParticleSystem explosionEffect;
        [SerializeField] private GameObject tankRenderers;
        [SerializeField] private float tankHealth = 200;
        [SerializeField] private float respawnTime = 3;

        private TankController m_TankController;
        private MeshRenderer m_MeshRenderer;
        private Vector3 m_OriginPosition;
        [SerializeField]private float m_CurrentHealth;
        private bool m_IsDead;

        private void Awake()
        {
            m_TankController = GetComponent<TankController>();
        }

        private void Start()
        {
            //Menyimpan posisi awal tank
            m_OriginPosition = transform.position;
        }

        private void OnEnable()
        {
            ResetTankHealth();
            //Mereset kalau tank hidup
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
            m_TankController.OnDeath(); //Mengehentikan gerakan tank
        
            //IENumerator akan berhenti sesuai durasi dari efek ledakan
            ParticleSystem.MainModule mainModule = explosionEffect.main;
            yield return new WaitForSeconds(mainModule.duration);

            //Memulai coroutine untuk merespawn tank
            StartCoroutine(RespawnTank());
        }

        private IEnumerator RespawnTank()
        {
            transform.position = m_OriginPosition; //Mengembalikan posisi tank ke posisi awal
        
            yield return new WaitForSeconds(respawnTime);
        
            tankRenderers.SetActive(true); //Setelah respawn time, mengaktifkan tank kembali
            m_TankController.NormalTankSpeed(); //Mengembalikan kecepatan tank
        }

        public void ActivateShield()
        {
            tankShield.SetActive(true);
            gameObject.tag = "Shield";
        }

        public void DeactivateShield()
        {
            gameObject.tag = "Tank";
            tankShield.SetActive(false);
        }
    }
}
