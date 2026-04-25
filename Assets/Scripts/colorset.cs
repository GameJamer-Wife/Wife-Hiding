using UnityEngine;

public class colorset : MonoBehaviour
{
    [SerializeField]
    private Color c;

    public void changePaint()
    {
        PaintGame.paint = c;
        Debug.Log("color set");
    }
}
