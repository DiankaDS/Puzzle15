using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private AudioSource fallBlock;

    public Point point;
    public int num;

    private void Start ()
    {
        fallBlock.Play();
    }

    public void InitNumber(int t)
    {
        num = t;
        text.text = t.ToString();
    }

    public void SetPosition(Point p)
    {
        point = p;
    }

    private void OnMouseDown()
    {
        Main.MainSingleton.MoveCube(this);
    }
}
