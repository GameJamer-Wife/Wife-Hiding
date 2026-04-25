using System;
using UnityEngine;

public class CuttingMinigame : SneakyMinigame
{
    [SerializeField]private GameObject knife;
    [SerializeField] private Vector2 basePos;

    private void Update()
    {
        knife.transform.position = basePos + (new Vector2(Mathf.Sin(Time.timeSinceLevelLoad*2), 0)*100);
        
    }
}
