using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainScreen
{
    public class GameOverShowScript : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverObject;
        [SerializeField] private TextMeshProUGUI gameOverHeader;
        [SerializeField] private TextMeshProUGUI gameOverText;

        public static void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void ShowGameOver(string header, string text)
        {
            gameOverHeader.text = header;
            gameOverText.text = text;
            gameOverObject.SetActive(true);
        }
    }
}