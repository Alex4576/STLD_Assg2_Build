using System;
using UnityEngine;
using TMPro;

public class PlayerScript: MonoBehaviour
{
    // Score variables for each type of collectible, initialized to 0 at the start of the game
    int goldenScore = 0; // Store the player's score from collecting golden items, initialized to 0 at the start of the game
    int crystalScore = 0; // Store the player's score from collecting crystals, initialized to 0 at the start of the game
    int fuelScore = 0; // Store the player's score from collecting fuel, initialized to 0 at the start of the game
    int nailScore = 0; // Store the player's score from collecting nails, initialized to 0 at the start of the game

    [SerializeField]
    TextMeshProUGUI scoreText; // Reference to the UI text element that displays the player's score, assigned from the Unity Inspector

    [SerializeField]
    float interactDistance = 3f; // Maximum distance at which the player can interact with objects, editable from the Unity Inspector

    Camera playerCamera; // Reference to the player's camera, used for raycasting to detect interactable objects

    // Player's Health
    [SerializeField]
    public int maxHealth = 100; // Player's current health, editable from the Unity Inspector
    private int currentHealth; // Player's current health, initialized to the maximum health

    [SerializeField] TextMeshProUGUI healthText; // Reference to the UI text element that displays the player's health, assigned from the Unity Inspector
    
    // Screens
    [SerializeField] GameObject loseScreen; // Reference to the game object that represents the lose screen, assigned from the Unity Inspector
    [SerializeField] GameObject winScreen; // Reference to the game object that represents the win screen, assigned from the Unity Inspector
    [SerializeField] TextMeshProUGUI finalHealth; // Reference to the UI text element that displays the player's final health on the win screen, assigned from the Unity Inspector
    [SerializeField] GameObject dotImage; // Reference to the game object that represents the dot image, assigned from the Unity Inspector, used for guide for raycasting    
    void Start()
    {
        UpdateScoreUI(); // Initialize the score display at the start of the game
        playerCamera = Camera.main; // Get the main camera in the scene, which is assumed to be the player's camera

        currentHealth = maxHealth; // Set the player's current health to the maximum health at the start of the game
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth; // Initialize the health display to show the player's current health and maximum health
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    // Damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce the player's current health by the specified damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure the player's health does not go below 0 or above the maximum health
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + "/" + maxHealth; // Update the health display to show the current health and maximum health
        }
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth); // Log a message indicating how much damage the player took and their current health

        if (currentHealth <= 0)
        {
            Debug.Log("You died."); // Log a message when the player's health reaches 0 or below, indicating that the player has died
            // Game over
            ShowLoseScreen(); // Call a method to show the game over screen (this method would need to be implemented separately)
        }
    }

    void ShowLoseScreen()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
            if (scoreText != null) scoreText.gameObject.SetActive(false); // Hide the score text when the lose screen is shown
            if (healthText != null) healthText.gameObject.SetActive(false); // Hide the health text when the lose screen is shown
            if (dotImage != null) dotImage.SetActive(false); // Hide the dot image when the lose screen is shown
            Time.timeScale = 0f ; // Pause the game when the lose screen is shown
        }
    }

    void ShowWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            finalHealth.text = "Your Final Health: " + currentHealth + "/" + maxHealth; // Update the final health display on the win screen to show the player's current health and maximum health
            if (scoreText != null) scoreText.gameObject.SetActive(false); // Hide the score text when the win screen is shown
            if (healthText != null) healthText.gameObject.SetActive(false); // Hide the health text when the win screen is shown
            if (dotImage != null) dotImage.SetActive(false); // Hide the dot image
            Time.timeScale = 0f ; // Pause the game when the win screen is shown
        }
    }

    void OnInteract()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            // Golden Crystal collectible
            if (hit.collider.CompareTag("Golden"))
            {
            Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                goldenScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Golden score: " + goldenScore); // Log the player's current golden score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
            }
            }

            // Crystal collectible
            if (hit.collider.CompareTag("Crystal"))
            {
            Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                crystalScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Crystal score: " + crystalScore); // Log the player's current crystal score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
            }
            }

            // Fuel collectible
            if (hit.collider.CompareTag("Fuel"))
            {
            Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                fuelScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Fuel score: " + fuelScore); // Log the player's current fuel score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
            }
            }

            // Nail collectible
            if (hit.collider.CompareTag("Nail"))
            {
            Collectible collectible = hit.collider.GetComponentInParent<Collectible>();
            if (collectible != null)
            {
                nailScore += collectible.collectibleScore; // Increase the player's score by the value of the collected item
                Debug.Log("Nail score: " + nailScore); // Log the player's current nail score after collecting an item
                UpdateScoreUI(); // Update the score display
                collectible.Collect(); // Call the Collect method on the collectible to handle its collection logic (e.g., play sound, destroy object)
                return; // Exit the method after collecting an item to prevent multiple interactions in one frame
            }
            }

            // Check if we hit the goal area as well as collected all required items
            if (hit.collider.CompareTag("GoalArea"))
            {
                hit.collider.GetComponent<AudioSource>().Play(); // Play a sound effect when the player interacts with the goal area
                if (crystalScore >= 9 && fuelScore >= 15 && nailScore >= 22 && goldenScore >= 1) // Check if the player has collected at least 1 of each required item
                {
                    Debug.Log("Spaceship is fixed, ready to launch... Let's go Home!"); // Log a winning message if the player reaches the goal area with enough points
                    ShowWinScreen(); // Call a method to show the win screen (this method would need to be implemented separately)
                }
                else
                {
                    Debug.Log("System Error: Please collect all required items."); // Log a message if the player reaches the goal area but does not have enough points
                }
            }
        }
        else
        {
            Debug.Log("No interactable object within range"); // Log a message if the player tries to interact but there is no object within the specified distance
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Crystal: " + crystalScore + "/9\nFuel: " + fuelScore + "/15\nNail: " + nailScore + "/22\nGolden Crystal: " + goldenScore + "/1"; // Update the score display to show all score types
    }

    // Damage over time
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            DamageObject dmg = other.GetComponent<DamageObject>();
            if (dmg != null)
            {
                TakeDamage(dmg.damageAmount); // Call the TakeDamage method with the damage amount from the DamageObject when the player enters a trigger with the "Damage" tag
            }
        }
    }
}