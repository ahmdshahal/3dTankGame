using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrateSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cratePrefab;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int maxPool = 3;
    [SerializeField] private Transform[] spawnPoint;
    
    private List<GameObject> m_CratePool= new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject crate = Instantiate(cratePrefab, transform);
            crate.SetActive(false);
            m_CratePool.Add(crate);
        }

        StartCoroutine(SpawnCrates());
    }

    private IEnumerator SpawnCrates()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnCrate();
        }
    }

    private void SpawnCrate()
    {
        for (int i = 0; i < m_CratePool.Count; i++)
        {
            if (!m_CratePool[i].activeInHierarchy)
            {
                Transform spawnPosition = GetRandomSpawnPosition();

                if (IsAvailableSpawnPosition(spawnPosition))
                {
                    m_CratePool[i].transform.position = spawnPosition.position;
                    m_CratePool[i].SetActive(true);
                    break;
                }
            }
        }
    }
    
    bool IsAvailableSpawnPosition(Transform position)
    {
        for (int i = 0; i < m_CratePool.Count(); i++)
        {
            if (m_CratePool[i].activeInHierarchy && m_CratePool[i].transform.position == position.position)
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
