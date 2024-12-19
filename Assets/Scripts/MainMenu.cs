using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName;

    public void Play() {
        Debug.Log("Loading game scene");
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit() {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}