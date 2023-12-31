using System.Collections.Generic;
using UnityEngine;

namespace Tank
{
    public class TankShooter : MonoBehaviour
    {
        [SerializeField] private Transform fireTransform; // Posisi spawn misil
        [SerializeField] private GameObject missilePrefab; // Prefab misil
        [SerializeField] private int maxAmmo = 7; // Jumlah maksimum amunisi
    
        private int m_CurrentAmmo; // Jumlah amunisi saat ini
        private List<GameObject> m_MissilePool = new List<GameObject>();

        private void Start()
        {
            // Inisialisasi pool misil
            for (int i = 0; i < maxAmmo; i++)
            {
                GameObject missile = Instantiate(missilePrefab);
                missile.SetActive(false);
                m_MissilePool.Add(missile);
            }

            m_CurrentAmmo = maxAmmo; // Mengatur jumlah amunisi awal
        }

        public void FireMissile()
        {
            if (m_CurrentAmmo > 0)
            {
                GameObject missile = GetMissileFromPool();
                if (missile != null)
                {
                    missile.transform.position = fireTransform.position;
                    missile.transform.rotation = fireTransform.rotation;
                    missile.SetActive(true);
                    m_CurrentAmmo--;
                }
            }
        }

        private GameObject GetMissileFromPool()
        {
            foreach (GameObject missile in m_MissilePool)
            {
                if (!missile.activeInHierarchy)
                    return missile;
            }
            return null; // Mengembalikan null jika tidak ada misil yang tersedia
        }

        public void ReloadAmmo()
        {
            m_CurrentAmmo = maxAmmo;
        }
    }
}
