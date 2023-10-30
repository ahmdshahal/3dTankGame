using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Power_Up
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnDelay; // Jeda waktu power up akan muncul
        [SerializeField] private GameObject[] powerUpPrefabs; // Prefab-prefab power up
        [SerializeField] private Transform[] spawnPoint; // Titik-titik di mana saja power up akan muncul

        private List<GameObject> m_PowerUpPool = new List<GameObject>();

        private void Start()
        {
            // Inisialisasi pool power up
            for (int i = 0; i < powerUpPrefabs.Length; i++)
            {
                GameObject powerUp = Instantiate(powerUpPrefabs[i], transform);
                powerUp.SetActive(false);
                m_PowerUpPool.Add(powerUp);
            }

            StartCoroutine(SpawnPowerUps());
        }

        private IEnumerator SpawnPowerUps()
        {
            // Akan spawn power up terus menerus dengan jeda
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                SpawnPowerUp();
            }
        }

        private void SpawnPowerUp()
        {
            // Mengambil index power up yang akan muncul
            int powerUpIndex = GetRandomPowerUp();

            if (!m_PowerUpPool[powerUpIndex].activeInHierarchy) // Mengecek jika power up belum dimunculkan
            {
                Transform spawnPosition = GetRandomSpawnPosition();

                //Jika titik yang dirandom tersedia, maka akan memunculkan power up di titik tersebut
                if (IsAvailableSpawnPosition(spawnPosition))
                {
                    m_PowerUpPool[powerUpIndex].transform.position = spawnPosition.position;
                    m_PowerUpPool[powerUpIndex].SetActive(true);
                }
            }
        }

        bool IsAvailableSpawnPosition(Transform position)
        {
            // Mengecek apakah titik kemunculan sudah ada power up yang dimunculkan atau tidak
            for (int i = 0; i < m_PowerUpPool.Count; i++)
            {
                if (m_PowerUpPool[i].activeInHierarchy && m_PowerUpPool[i].transform.position == position.position)
                {
                    return false;
                }
            }

            return true;
        }
    
        private Transform GetRandomSpawnPosition()
        {
            // Mengacak titik power up dimunculkan
            int randomIndex = Random.Range(0, spawnPoint.Length);
            return spawnPoint[randomIndex];
        }

        private int GetRandomPowerUp()
        {
            // Mengacak power up yang akan dimunculkan
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            return randomIndex;
        }
    }
}
