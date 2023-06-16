using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public int width = 10;
    public int height = 10;

    public GameObject wall;
    public GameObject player;
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject waypointPrefab;

    private bool playerSpawned = false;

    // Use this for initialization
    void Start () {
        GenerateLevel();
    }
    
    // Create a grid based level
    void GenerateLevel()
    {
        // Loop over the grid
        for (int x = 0; x <= width; x+=2)
        {
            for (int y = 0; y <= height; y+=2)
            {
                // Should we place a wall?
                if (Random.value > .7f)
                {
                    // Spawn a wall
                    Vector3 pos = new Vector3(x - width / 2f, 1f, y - height / 2f);
                    Instantiate(wall, pos, Quaternion.identity, transform);
                } else if (!playerSpawned) // Should we spawn a player?
                {
                    // Spawn the player
                    Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
                    Instantiate(player, pos, Quaternion.identity);
                    playerSpawned = true;
                } else if (Random.value > .5f) // Should we spawn an enemy?
                {
                    // Determine which enemy to spawn
                    GameObject enemyPrefab = Random.value > .5f ? enemyType1 : enemyType2;

                    // Spawn the enemy
                    Vector3 pos = new Vector3(x - width / 2f, 1.25f, y - height / 2f);
                    GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

                    // Spawn waypoints for the enemy's patrol path
                    for (int i = 0; i < 3; i++)
                    {
                        pos = new Vector3(Random.Range(x - 4f, x + 4f) - width / 2f, 1.25f, Random.Range(y - 4f, y + 4f) - height / 2f);
                        Instantiate(waypointPrefab, pos, Quaternion.identity, enemy.transform);
                    }
                }
            }
        }
    }
}