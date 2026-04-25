using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Knit : SneakyMinigame
{
    [SerializeField]
    private int times;

    [SerializeField] private List<GameObject> arrows = new List<GameObject>();

    private int[] directions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directions = new int[times];
        directions[0] = Random.Range(0, 3);
        for (int i = 1; i < directions.Length; i++)
        {
            directions[i] = Random.Range(0, 3);
            if (directions[i - 1] == directions[i])
            {
                directions[i]++;
                directions[i] %= 4;
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (progress >= times)
        {
            Destroy(gameObject);
            DoingSecretStuff = false;
            return;
        }
        checkProgress();
        
        //makes arrows look the right way
        for (int i = 0; i < arrows.Count; i++)
        {
            //checks the equivalent index in the direction list
            int directionEquiv = progress+i;
            //make sure the direction exists
            if (directionEquiv >= directions.Length)
            {
                arrows[i].SetActive(false);
                continue;
            }
            arrows[i].SetActive(true);
            //set rotation here dummy
            arrows[i].transform.rotation=Quaternion.Euler(0,0,90*directions[directionEquiv]);
        }
    }

    private void checkProgress()
    {
         
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && directions[progress] == 0)
        {
            progress++;
            return;
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && directions[progress] == 1)
        {
            progress++;
            return;
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))&& directions[progress] == 2)
        {
            progress++;
            return;
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && directions[progress] == 3)
        {
            progress++;
            return;
        }

        if (Input.anyKeyDown) punishment();
    }

    private void punishment()
    {
        if (progress == 0) return;
        progress--;
    }
}
