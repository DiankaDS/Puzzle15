using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void StartGame()
    {
        PlayAudio();
        LoadScene(false);
    }

    public void StartTraining()
    {
        PlayAudio();
        LoadScene(true);
    }

    public void OnMenu()
    {
        PlayAudio();
        SceneManager.LoadScene("Menu");
    }

    private void LoadScene(bool flag)
    {
        StaticData.SetIsTrainingFlag(flag);
        SceneManager.LoadScene("Main");
    }

    private void PlayAudio() {
        click.Play();
    }
}
