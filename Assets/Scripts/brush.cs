using UnityEngine;
using UnityEngine.UI;

public class brush : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private Image tip;

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Input.mousePosition+offset;
        tip.color = PaintGame.paint;
    }
}
