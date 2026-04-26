using MainScreen;
using UnityEngine;

public class SneakyMinigame : MonoBehaviour
{
    public static bool DoingSecretStuff;

    [SerializeField] private StartGameScript startGameScript;
    [SerializeField] private GameType gameType;

    private bool minigameWasActive;
    protected int progress;

    private void OnEnable()
    {
        DoingSecretStuff = true;
        minigameWasActive = true;
    }

    private void OnDisable()
    {
        DoingSecretStuff = false;
    }

    private void OnDestroy()
    {
        if (!minigameWasActive) return;

        var startGameScript = FindObjectOfType<StartGameScript>();
        if (startGameScript != null) startGameScript.EndGame();
    }

    protected void EndMinigame()
    {
        Destroy(gameObject);
        startGameScript.EndAndDisableGame(gameType);
        DoingSecretStuff = false;
    }
}