using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingMinigame : SneakyMinigame
{
    [SerializeField]private GameObject knife;
    [SerializeField] private Vector2 basePos;
    [SerializeField] private Animator animaot;
    [SerializeField] private Animator stemAnimaot;
    private float loopingNum=0;
    public static bool cutting;
    private bool cuttingLastTime = false;
    private bool flowerFresh = true;
    private int goal = 4;
    [SerializeField] private Image flower;
    [SerializeField] private List<Sprite> flowers = new List<Sprite>();
    private static bool flowerChange = false;


    private void Update()
    {
        float tuscany = Mathf.Sin(loopingNum * 2+(progress*0.5f));
        if (!cutting)
        {
            loopingNum += Time.deltaTime;
            if (cuttingLastTime)
            {
                cuttingLastTime = false;
                if (tuscany * tuscany < 0.1f && flowerFresh)
                {
                    progress++;
                    if (progress > goal)
                    {
                        EndMinigame();
                        return;
                    }
                    
                    stemAnimaot.SetTrigger("ccc");
                    Debug.Log("cuted");
                    tuscany = Mathf.Sin(loopingNum * 2+(progress*0.5f));
                }
            }
        }
        else cuttingLastTime=true;
        
        knife.transform.position = basePos + new Vector2(tuscany, 0)*100;
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            cutting = true;
            animaot.SetTrigger("ccc");
        }

        if (flowerChange)
        {
            flowerChange = false;
            flower.sprite = flowers[progress%flowers.Count];
        }

    }

    public static void toggleCutting()
    {
        cutting = false;
    }

    public static void stemd()
    {
        flowerChange = true;
    }
}
