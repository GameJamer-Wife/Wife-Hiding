using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class coldot : MonoBehaviour
{
    public Vector2 guide;
    [SerializeField] private Vector2 offset;
    
    [SerializeField] private RectTransform rect;
    public Color color;

    private void Start()
    {
        PaintGame.dots++;
    }

    // Update is called once per frame
    /*void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, cam, out guide);
        if ((guide-offset).magnitude < 7)
        {
            if(color==PaintGame.paint)
            {
                Destroy(this.gameObject);
                PaintGame.dots--;
            }
            Debug.Log((guide-offset).magnitud   e);
        }
    }*/

    public void OnPointerEnter()
    {
        if(color==PaintGame.paint)
        {
            Destroy(this.gameObject);
            PaintGame.dots--;
        }
    }
   
}
