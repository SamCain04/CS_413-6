using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public float score = 100;

    private Transform player; // Reference to the player's transform
    private bool moveTowardPlayer = false; // Flag to determine if enemy should move toward the player
    private string difficulty = "Easy"; // Default difficulty

    public Vector3 pos
    {
        get => transform.position;
        set => transform.position = value;
    }

    public virtual void Move()
    {
        if (moveTowardPlayer && player != null)
        {
            // Calculate direction toward the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Adjust speed based on difficulty
            float adjustedSpeed = difficulty switch
            {
                "Easy" => 0f,                              // No movement
                "Medium" => speed / 2,                     // Slow movement
                "Hard" => speed,                           // Fast movement
                _ => speed / 2                             // Default to medium
            };

            // Apply the movement
            pos += direction * adjustedSpeed * Time.deltaTime;
        }
        else
        {
            // Default movement (downward)
            Vector3 tempPos = pos;
            tempPos.y -= speed * Time.deltaTime;
            pos = tempPos;
        }
    }

    void Update()
    {
        Move();

        // Destroy the enemy if it moves out of bounds
        if (pos.y < -BoundsCheck.camHeight)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGo = coll.gameObject;
        if (otherGo.GetComponent<ProjectileHero>() != null)
        {
            Destroy(otherGo);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enemy hit by non-ProjectileHero: " + otherGo.name);
        }
    }

    // Method to set the difficulty
    public void SetDifficulty(string level)
    {
        difficulty = level;

        switch (difficulty)
        {
            case "Easy":
                moveTowardPlayer = false; // Enemy stays stationary
                Debug.Log("Difficulty set to Easy");
                break;

            case "Medium":
                moveTowardPlayer = true; // Slow movement toward the player
                Debug.Log("Difficulty set to Medium");
                break;

            case "Hard":
                moveTowardPlayer = true; // Fast movement toward the player
                Debug.Log("Difficulty set to Hard");
                break;

            default:
                Debug.LogWarning("Unknown difficulty level: " + difficulty);
                break;
        }
    }

    // Method to assign the player as the target
    public void SetPlayerTarget(Transform targetPlayer)
    {
        player = targetPlayer;
    }
}
