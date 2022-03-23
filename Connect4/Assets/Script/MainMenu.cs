using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameLevel;
    public GameObject settingsWindow;

    // Lance le jeu
    public void playGame()
    {
        SceneManager.LoadScene(this.GameLevel);
    }

    // Permet d'ouvrir les settings du jeu 
    public void openSettings()
    {
        settingsWindow.SetActive(true);
    }

    // Quitte le jeu
    public void quitGame(){
        Application.Quit();
    }
}
