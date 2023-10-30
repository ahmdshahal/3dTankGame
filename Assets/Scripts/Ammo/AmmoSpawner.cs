using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int maxPool = 3;
    [SerializeField] private Transform[] spawnPoint;
    
    private List<GameObject> m_AmmoPool= new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject crate = Instantiate(ammoPrefab, transform);
            crate.SetActive(false);
            m_AmmoPool.Add(crate);
        }

        StartCoroutine(SpawnAmmos());
    }

    private IEnumerator SpawnAmmos()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnAmmo();
        }
    }

    private void SpawnAmmo()
    {
        for (int i = 0; i < m_AmmoPool.Count; i++)
        {
            if (!m_AmmoPool[i].activeInHierarchy)
            {
                Transform spawnPosition = GetRandomSpawnPosition();

                if (IsAvailableSpawnPosition(spawnPosition))
                {
                    m_AmmoPool[i].transform.position = spawnPosition.position;
                    m_AmmoPool[i].SetActive(true);
                    break;
                }
            }
        }
    }
    
    bool IsAvailableSpawnPosition(Transform position)
    {
        for (int i = 0; i < m_AmmoPool.Count(); i++)
        {
            if (m_AmmoPool[i].activeInHierarchy && m_AmmoPool[i].transform.position == position.position)
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
}
