using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;

    [Header("Inscribed")]
    public GameObject[] prefabEnemies; // Array of enemy prefabs
    public float enemySpawnPerSecond = 0.5f; // Default spawn rate
    public float enemyInsetDefault = 1.5f; // Padding for enemy spawning
    public float gameRestartDelay = 2f;

    private void Awake()
    {
        S = this;
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    private void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }

    private void Restart()
    {
        SceneManager.LoadScene("__Scene_0");
    }

    static public void HERO_DIED()
    {
        S.DelayedRestart();
    }

    public void SpawnEnemy()
    {
        // Spawn a random enemy from the prefab array
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate(prefabEnemies[ndx]);

        // Ensure the enemy spawns within screen bounds
        float enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        Vector3 pos = Vector3.zero;
        float xMin = -BoundsCheck.camWidth + enemyInset;
        float xMax = BoundsCheck.camWidth - enemyInset;
        pos.x = Random.Range(xMin, xMax);
        pos.y = BoundsCheck.camHeight + enemyInset;
        go.transform.position = pos;

        // Schedule the next spawn
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    public void SetLevel(string level)
    {
        // Adjust spawn rate based on difficulty
        switch (level)
        {
            case "Easy":
                enemySpawnPerSecond = 0.5f; // Slow spawn rate
                break;

            case "Medium":
                enemySpawnPerSecond = 1.0f; // Moderate spawn rate
                break;

            case "Hard":
                enemySpawnPerSecond = 2.0f; // Fast spawn rate
                break;

            default:
                Debug.LogWarning($"Unknown difficulty level: {level}");
                return;
        }

        Debug.Log($"Spawn rate adjusted to {enemySpawnPerSecond} enemies/second for {level} level.");
    }
}
