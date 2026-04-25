using System;
using UnityEngine;

public class SneakyMinigame : MonoBehaviour
{
    public static bool DoingSecretStuff = false;
    protected int progress;

    private void OnEnable()
    {
        DoingSecretStuff = true;
    }

    private void OnDisable()
    {
        DoingSecretStuff = false;
    }

    protected void EndMinigame()
    {
            Destroy(gameObject);
            DoingSecretStuff = false;
    }
}
