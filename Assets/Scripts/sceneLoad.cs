using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoad : MonoBehaviour
{
    [SerializeField] private int sceneId;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneId);
    }
}
