using UnityEngine;
using UnityEngine.SceneManagement;


namespace MainScreen
{
    public class GameOverShowScript : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverObject;
        [SerializeField] private TMPro.TextMeshProUGUI gameOverHeader;
        [SerializeField] private TMPro.TextMeshProUGUI gameOverText;
        
        public static void RestartGame()
        {
            var scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);

        }
        
    }
}
