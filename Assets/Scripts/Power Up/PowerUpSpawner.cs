using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Power_Up
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnDelay;
        [SerializeField] private GameObject[] powerUpPrefabs;
        [SerializeField] private Transform[] spawnPoint;

        private List<GameObject> m_PowerUpPool = new List<GameObject>();

        private void Start()
        {
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
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                SpawnPowerUp();
            }
        }

        private void SpawnPowerUp()
        {
            int powerUpIndex = GetRandomPowerUp();

            if (!m_PowerUpPool[powerUpIndex].activeInHierarchy)
            {
                Transform spawnPosition = GetRandomSpawnPosition();

                if (IsAvailableSpawnPosition(spawnPosition))
                {
                    m_PowerUpPool[powerUpIndex].transform.position = spawnPosition.position;
                    m_PowerUpPool[powerUpIndex].SetActive(true);
                }
            }
        }

        bool IsAvailableSpawnPosition(Transform position)
        {
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
            int randomIndex = Random.Range(0, spawnPoint.Length);
            return spawnPoint[randomIndex];
        }

        private int GetRandomPowerUp()
        {
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            return randomIndex;
        }
    }
}
