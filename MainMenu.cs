using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void PlayGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void Settings() {
        SettingsPanel.SetActive(true);
    }

    public void CloseSettings() {
        SettingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
