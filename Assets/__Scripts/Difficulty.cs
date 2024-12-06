using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Main mainScript; // Reference to the Main script
    private string currentDifficulty = "Easy"; // Default difficulty

    private void Start()
    {
        // Start periodically applying difficulty to all enemies
        InvokeRepeating(nameof(ApplyDifficultyToAllEnemies), 0f, 3f);

        // Initialize spawn rate based on the current difficulty
        if (mainScript != null)
        {
            mainScript.SetLevel(currentDifficulty);
        }
        else
        {
            Debug.LogError("Main script reference not assigned!");
        }
    }

    public void SetEasy()
    {
        currentDifficulty = "Easy";
        UpdateDifficulty();
    }

    public void SetMedium()
    {
        currentDifficulty = "Medium";
        UpdateDifficulty();
    }

    public void SetHard()
    {
        currentDifficulty = "Hard";
        UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        // Update the spawn rate via Main script
        if (mainScript != null)
        {
            mainScript.SetLevel(currentDifficulty);
        }
        else
        {
            Debug.LogError("Main script reference not assigned!");
        }

        // Update all active enemies to match the new difficulty
        ApplyDifficultyToAllEnemies();

        Debug.Log($"Difficulty set to {currentDifficulty}");
    }

    private void ApplyDifficultyToAllEnemies()
    {
        // Find all active enemies in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.SetDifficulty(currentDifficulty); // Apply difficulty to each enemy
            if (player != null)
            {
                enemy.SetPlayerTarget(player); // Ensure the player is the target
            }
        }

        Debug.Log($"Applied difficulty '{currentDifficulty}' to {enemies.Length} enemies.");
    }
}
