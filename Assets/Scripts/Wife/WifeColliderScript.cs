using UnityEngine;

namespace Wife
{
    public class WifeColliderScript : MonoBehaviour
    {
        [SerializeField] private GameObject wifeObject;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.name != wifeObject.name) return;

            Debug.Log("WIFE!!");
            if (SneakyMinigame.DoingSecretStuff) Debug.Log("Wife caught you doing secret stuff! Game over!");
        }
    }
}