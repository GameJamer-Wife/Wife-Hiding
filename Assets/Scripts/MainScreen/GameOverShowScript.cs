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

        [SerializeField] private GameObject timerGameObject;

        public static void RestartGame()
        {
            sceneLoad.LoadSceneStatic(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowGameOver(string header, string text)
        {
            gameOverHeader.text = header;
            gameOverText.text = text;
            gameOverObject.SetActive(true);
            
            Destroy(timerGameObject);
        }
    }
}