using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    [SerializeField] private float wallHealth = 100;

    public void TakeDamage(float amount)
    {
        //Mengurangi health saat ini dengan amount
        wallHealth -= amount;

        //Jika health habis, maka wall dinonaktifkan
        if (wallHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
