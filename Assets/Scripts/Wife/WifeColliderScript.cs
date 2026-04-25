using MainScreen;
using UnityEngine;

namespace Wife
{
    public class WifeColliderScript : MonoBehaviour
    {
        [SerializeField] private GameObject wifeObject;
        [SerializeField] public GameOverShowScript gameOverManager;


        private void Start()
        {
            if (wifeObject == null)
            {
                Debug.LogError("Wife object is not assigned in the inspector!");
            }

            if (gameOverManager == null)
            {
                Debug.LogError("GameOverShowScript is not assigned in the inspector!");
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.name != wifeObject.name) return;

            Debug.Log("WIFE!!");
            if (SneakyMinigame.DoingSecretStuff)
            {
                gameOverManager.ShowGameOver("Caught!", "You got caught by your wife! Better luck next time.");
            }
        }
    }
}