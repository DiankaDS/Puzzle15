using UnityEngine;
using UnityEngine.UI;

public class OnWin : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private AudioSource winAudio;

    private void Start()
    {
        Main.MainSingleton.OnWin += SetWin;
    }

    public void SetWin()
    {
        text.text = "You Win!";
        winAudio.Play();
        Main.MainSingleton.OnWin -= SetWin;
    }
}
