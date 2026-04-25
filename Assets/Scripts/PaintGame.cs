using System;
using UnityEngine;
using UnityEngine.UI;

public class PaintGame : MonoBehaviour
{
    [SerializeField]
    private ComputeShader PainterShader;
    [SerializeField]
    private ComputeShader TemplateShader;

    [SerializeField] private RawImage disp;
    [SerializeField] private RenderTexture writeTex;
    [SerializeField] private Texture2D referenceTex;
    public Color paint;

    [SerializeField] private Vector2 guide=Vector2.zero;
    [SerializeField] private Camera cam;
    [SerializeField]
    private RectTransform rect;
    private void Start()
    {
        //disp = GetComponent<RawImage>();
        RenderTexture texture = new RenderTexture(referenceTex.width, referenceTex.height,0);
        texture.enableRandomWrite = true;
        texture.Create();
        //make template version
        TemplateShader.SetTexture(0,"Result", texture);
        TemplateShader.SetTexture(0,"base", referenceTex);
        TemplateShader.Dispatch(0, referenceTex.width/8,referenceTex.height/8, 1);
        
        writeTex = texture;
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, cam, out guide);
        guide -= new Vector2(-120, -120);
        Run(guide);
        checkColor();
    }

    public void Run(Vector2 place)
    {
       PainterShader.SetTexture(0,"Result", writeTex);
       PainterShader.SetTexture(0,"base", referenceTex);
       PainterShader.SetVector("brush",place);
       PainterShader.SetVector("paint",paint);
       PainterShader.Dispatch(0, writeTex.width/8,writeTex.height/8, 1);
       disp.texture = writeTex;
    }

    private void checkColor()
    {
        /*for (int i = 0; i < writeTex.width; i++)
        {
            for (int j = 0; j < writeTex.height; j++)
            {
                Vector2 thPos = new Vector2(i, j);
                
                if(((Texture2D)disp.texture).GetPixel())
                
            }
        }*/
        Texture2D tex=new Texture2D(256,256);
        Graphics.Blit(disp.texture,tex.graphicsTexture ,Vector2.one,Vector2.zero);
        Color[] cls=tex.GetPixels();
        Color[] cls2 = referenceTex.GetPixels();
        int t=0;
        for (int i = 0; i < cls.Length; i++)
        {
            if (i > cls2.Length - 1) break;
            if (cls[i] == cls2[i]) t++;
        }

        float cor = t / cls.Length;
        Debug.Log(cor);
        if(cor>0.5f)Debug.Log("sdfghi");
    }
}
