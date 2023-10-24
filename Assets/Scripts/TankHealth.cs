using System.Collections;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private GameObject tankRenderers;
    [SerializeField] private float tankHealth = 200;
    [SerializeField] private float respawnTime = 3;

    private TankController m_TankController;
    private MeshRenderer m_MeshRenderer;
    private Vector3 m_OriginPosition;
    private float m_CurrentHealth;
    private bool m_Dead;

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
        //Mereset health saat ini
        m_CurrentHealth = tankHealth;
        //Mereset kalau tank hidup
        m_Dead = false;
        tankRenderers.SetActive(true);
    }

    public void TakeDamage(float amount)
    {
        //Mengurangi health saat ini dengan amount
        m_CurrentHealth -= amount;

        //Jika health saat ini kurang dari 0 dan tidak sedang dead maka tank akan mati
        if (m_CurrentHealth <= 0 & !m_Dead)
        {
            StartCoroutine(OnDeath());
        }
    }

    private IEnumerator OnDeath()
    {
        m_Dead = true;

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
    }
}
