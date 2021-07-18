using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonStart : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void RestartGame()
    {
        click.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
