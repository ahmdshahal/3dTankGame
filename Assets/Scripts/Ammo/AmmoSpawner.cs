using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab; // Prefab ammo
    [SerializeField] private float spawnDelay; // Jeda waktu ammo akan muncul
    [SerializeField] private int maxPool = 3; //Jumlahh maksimum ammo yang akan muncul
    [SerializeField] private Transform[] spawnPoint; // Titik-titik di mana saja ammo akan muncul
    
    private List<GameObject> m_AmmoPool= new List<GameObject>();

    private void Start()
    {
        // Inisialisasi pool ammo
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
        // Akan spawn ammo terus menerus dengan jeda
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
                
                //Jika titik yang dirandom tersedia, maka akan memunculkan ammo di titik tersebut
                if (IsAvailableSpawnPosition(spawnPosition))
                {
                    m_AmmoPool[i].transform.position = spawnPosition.position;
                    m_AmmoPool[i].SetActive(true);
                    break; // Menghentikan for loop jika sudah ada ammo yang dimunculkan
                }
            }
        }
    }
    
    bool IsAvailableSpawnPosition(Transform position)
    {
        // Mengecek apakah titik kemunculan sudah ada ammo yang dimunculkan atau tidak
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
        // Mengacak titik muncul ammo yang akan dimunculkan
        int randomIndex = Random.Range(0, spawnPoint.Length);
        return spawnPoint[randomIndex];
    }
}
