using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winCanvas; // Canvas pop up menang
        [SerializeField] private TextMeshProUGUI playerText; // Text player yang menang

        public void Win(string player)
        {
            playerText.text = player; // Mengganti text dengan player yang menang
            winCanvas.SetActive(true); // Mengaktifkan pop up menang
        }

        public void RestartGame()
        {
            // Memuat ulang scene yang aktif saat ini
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
