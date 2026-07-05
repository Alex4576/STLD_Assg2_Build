using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel; // Reference to the main menu panel in the UI, set in the Unity Inspector
    public GameObject gamePanel; // Reference to the game panel in the UI, set in the Unity Inspector

    void Start()
    {
        menuPanel.SetActive(true); // Show the main menu panel when the game starts
        gamePanel.SetActive(false); // Hide the game panel when the game starts
        Time.timeScale = 0; // Pause the game when the main menu is active
    }
    public void OnStartClick()
    {
        menuPanel.SetActive(false); // Hide the main menu panel when the Start button is clicked, allowing the game to begin
        gamePanel.SetActive(true); // Show the game panel when the Start button is clicked
        Time.timeScale = 1; // Resume the game when the Start button is clicked
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the Unity Editor when the Exit button is clicked
#endif
        Application.Quit(); // Quit the application when the Exit button is clicked (this will work in a built application)
    }
}