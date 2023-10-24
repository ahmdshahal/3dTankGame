using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Missile : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private int missileDamage = 50; // Kerusakan yang disebabkan oleh misil
    [SerializeField] private float missileSpeed = 10.0f; // Kecepatan misil default

    private float m_CurrentMissileSpeed; //Kecepatan misil realtime
    private float m_Lifetime = 2.0f; // Waktu hidup misil
    private MeshRenderer m_MeshRenderer;

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        // Set waktu hidup misil
        Invoke("ReturnToPool", m_Lifetime);
        m_MeshRenderer.enabled = true;
        m_CurrentMissileSpeed = missileSpeed;
    }

    private void Update()
    {
        // Bergerak maju sesuai dengan kecepatan
        transform.Translate(Vector3.forward * (m_CurrentMissileSpeed * Time.deltaTime));
    }

    private void ReturnToPool()
    {
        // Mengembalikan misil ke pool
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Menghancurkan misil saat menabrak tembok atau tank lain
        // Anda dapat menambahkan logika lain di sini, seperti mengurangi kesehatan tank jika terkena
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Tank"))
        {
            StartCoroutine(Explode());
            if (collision.gameObject.TryGetComponent(out TankHealth tankHealth))
            {
                tankHealth.TakeDamage(missileDamage);
            }
        }
    }

    private IEnumerator Explode()
    {
        // Aktifkan efek ledakan
        explosionEffect.Play();
        
        m_MeshRenderer.enabled = false; //Menonaktifkan mesh dari misil
        m_CurrentMissileSpeed = 0; //Mengehentikan gerakan misil
        
        //IENumerator akan berhenti sesuai durasi dari efek ledakan
        ParticleSystem.MainModule mainModule = explosionEffect.main;
        yield return new WaitForSeconds(mainModule.duration);
        
        ReturnToPool();
    }

    private void OnDisable()
    {
        gameObject.transform.rotation = quaternion.identity;
    }
}
