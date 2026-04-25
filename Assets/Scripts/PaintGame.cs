using System;
using UnityEngine;

public class PaintGame : MonoBehaviour
{
    [SerializeField]
    private ComputeShader PainterShader;
    [SerializeField]
    private ComputeShader TemplateShader;

    [SerializeField] private Texture2D writeTex;
    [SerializeField] private Texture2D referenceTex;
    public Color paint;

    private void Start()
    {
        writeTex = new Texture2D(256, 256);
        //make image
        //make template version
    }

    public void Run(Vector4[] dots)
    {
       // RenderTexture texture = new RenderTexture(size, size,0);
       // writeTex.enableRandomWrite = true;
       // texture.Create();
        
       PainterShader.SetTexture(0,"Result", writeTex);
       PainterShader.SetTexture(0,"base", referenceTex);
       PainterShader.SetVector("brush",new Vector2(0,0));
       PainterShader.SetVector("paint",paint);
       PainterShader.Dispatch(0, writeTex.width/8,writeTex.height/8, 1);
    }
}
