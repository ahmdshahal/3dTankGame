using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winCanvas;
        [SerializeField] private TextMeshProUGUI playerText;

        public void Win(string player)
        {
            playerText.text = player;
            winCanvas.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
