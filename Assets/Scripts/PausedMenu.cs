using UnityEngine;

public class PausedMenu : MonoBehaviour
{
    public GameObject container;
    public GameObject menuPanel; // Reference to the main menu panel in the UI, set in the Unity Inspector
    public GameObject gamePanel; // Reference to the game panel in the UI, set in the Unity Inspector
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the Unity Editor when the Exit button is clicked
#endif
        Application.Quit(); // Quit the application when the Exit button is clicked (this will work in a built application)
    }
}
