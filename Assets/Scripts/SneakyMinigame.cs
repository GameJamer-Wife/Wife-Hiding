using System;
using MainScreen;
using UnityEngine;

public class SneakyMinigame : MonoBehaviour
{
    public static bool DoingSecretStuff = false;
    protected int progress;
    private bool minigameWasActive;
    [SerializeField]
    private StartGameScript startGameScript;

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
        if (!minigameWasActive)
        {
            return;
        }

        var startGameScript = FindObjectOfType<StartGameScript>();
        if (startGameScript != null)
        {
            startGameScript.EndGame();
        }
    }

    protected void EndMinigame()
    {
            Destroy(gameObject);
            startGameScript.EndGame();
            DoingSecretStuff = false;
    }
}
