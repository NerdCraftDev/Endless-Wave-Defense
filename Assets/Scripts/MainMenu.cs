using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string menuSceneName;
    public string gameSceneName;

    public async void Play() {
        Debug.Log("Loading game scene");
        await SceneManager.LoadSceneAsync(gameSceneName);
    }

    public async void Menu() {
        Debug.Log("Loading menu scene");
        await SceneManager.LoadSceneAsync(menuSceneName);
    }

    public void Quit() {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}