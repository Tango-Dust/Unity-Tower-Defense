using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTester : MonoBehaviour
{
    private GameObject[] randomlySelectedPrefabs = new GameObject[6]; // Array of randomly selected enemy prefabs
    [SerializeField]
    private GameObject[] enemies = new GameObject[6]; // Array of all available enemy prefabs
    private GameObject initEnemy; // Used to set the difficulty level
    private Vector3 spawnLocation = new Vector3();
    private System.Random rnd = new System.Random();
    private int rndSelectedEnemy;
    private int count = 0; // Used to control how many enemies are spawned and at what rate they are spawned
    private int currentWave = 1;
    [SerializeField]
    private int enemyCount = 1;
    private bool startChecking = false;
    [SerializeField]
    private bool startWave = false;
    [SerializeField]
    private string difficulty = "easy";
    [SerializeField]
    private GameObject startNextWave;
    

    // Use this for initialization
    void Start ()
    {
        initEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if(difficulty == "easy")
        {
            initEnemy.GetComponent<EnemyController>().SetDifficulty(1.0f, 1.0f, 1.0f, 10.0f);
        }

        else if(difficulty == "intermediate")
        {
            initEnemy.GetComponent<EnemyController>().SetDifficulty(1.5f, 1.5f, 1.5f, 10.0f);
        }

        else
        {
            initEnemy.GetComponent<EnemyController>().SetDifficulty(2.0f, 2.0f, 2.0f, 10.0f);
        }

        // Initial enemy selection
        SelectEnemy();

        // Grab current spawn location
        spawnLocation = getSpawnLocation();
    }

    // Update is called once per frame
    public void Update()
    {
        Wave(currentWave);
        count++;

        if(startChecking == true)
        {
            if(IsWaveDone() == true)
            {
                // Display start next wave button
                startNextWave.SetActive(true);

                if (startWave == true)
                {
                    // Stop Displaying start next wave button
                    startNextWave.SetActive(false);

                    currentWave++;

                    count = 0;
                 
                    SelectEnemy(); // Randomly select new enemy(s) for the new wave
                    
                    startChecking = false; // Stop checking for enemies until the next wave has completed and player selects "start wave"
                    startWave = false;
                }
            }
        }
    }

    public void OnButtonClicked()
    {
        startWave = true;
    }

    public int GetEnemycount()
    {
        return enemyCount;
    }

    public void EnemyCountDecrement()
    {
        enemyCount--;
    }

    // Used to start next wave
    private void SetStartWave()
    {
        startWave = true;
    }

    // Find the location of the spawner object for the current map
    private Vector3 getSpawnLocation()
    {
        Vector3 spawn;
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawn");
        spawn = spawner.gameObject.transform.position;
        return spawn;
    }

    // Randomly select a set of enemies that can be spawned in during the current wave
    private void SelectEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            rndSelectedEnemy = rnd.Next(0, 6); // Create a random number between 0 and 2
            
            // Use that number to select an index from the enemies array to randomly pick an enemy and place it into an array of enemies that can be spawned during the current wave
            randomlySelectedPrefabs[i] = enemies[rndSelectedEnemy];
        }

    }

    // Instantiate a single enemy 
    private void CreateEnemy(GameObject prefab)
    {
        Instantiate(prefab, spawnLocation, Quaternion.identity);
        enemyCount++;
    }


    public int GetCurrentWave()
    {
        return currentWave;
    }

    // Checks to see if the wave is done. If the wave is done, increase the wave counter
    // TODO create an ending condition
    private bool IsWaveDone()
    {
        if (enemyCount == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    // Spawn enemies in. Changes how many enemies are spawned in, and how quickly depending on what wave the player is currently at and difficulty level
    private void Wave(int currentW)
    {
        if (difficulty == "easy")
        {
            if (currentW < 5)
            {
                if (count <= 1000 && count % 60 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                }

                // Wave is finished spawning, start checking for all enemies from previous wave to be destoryed
                else if (count > 1000) { startChecking = true; }


            }

            else if (currentW >= 5 && currentW < 7)
            {
                if (count <= 1000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                }

                // Wave is finished spawning, start checking for all enemies from previous wave to be destoryed
                else if (count > 1000) { startChecking = true; }
            }

            else if (currentW >= 7 && currentW < 12)
            {
                if (count <= 1000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                    CreateEnemy(randomlySelectedPrefabs[2]);
                }

                else if (count > 1000) { startChecking = true; }
            }

            else if (currentW >= 12 && currentW < 15)
            {
                if (count <= 2000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                }

                else if (count > 2000) { startChecking = true; }
            }

            else
            {
                // TODO victory screen
            }
        }

        else if (difficulty == "intermediate")
        {
            if (currentW < 5)
            {
                if (count <= 2000 && count % 60 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                }

                // Wave is finished spawning, start checking for all enemies from previous wave to be destoryed
                else if (count > 2000) { startChecking = true; }
            }

            else if (currentW >= 5 && currentW < 7)
            {
                if (count <= 2000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                }

                // Wave is finished spawning, start checking for all enemies from previous wave to be destoryed
                else if (count > 2000) { startChecking = true; }
            }

            else if (currentW >= 7 && currentW < 12)
            {
                if (count <= 2000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                    CreateEnemy(randomlySelectedPrefabs[2]);
                }

                else if (count > 2000) { startChecking = true; }
            }

            else if (currentW >= 12 && currentW < 15)
            {
                if (count <= 4000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                }

                else if (count > 4000) { startChecking = true; }
            }

            else
            {
                // TODO victory screen
            }
        }

        else
        {
            if (currentW < 5)
            {
                if (count <= 2000 && count % 60 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                }

                // Wave is finished spawning, start checking for all enemies from previous wave to be destoryed
                else if (count > 2000) { startChecking = true; }
            }

            else if (currentW >= 5 && currentW < 7)
            {
                if (count <= 2000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                    CreateEnemy(randomlySelectedPrefabs[2]);
                }

                // Wave is finished spawning, start checking for all enemies from previous wave to be destoryed
                else if (count > 2000) { startChecking = true; }
            }

            else if (currentW >= 7 && currentW < 12)
            {
                if (count <= 2000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                    CreateEnemy(randomlySelectedPrefabs[2]);
                }

                else if (count > 2000) { startChecking = true; }
            }

            else if (currentW >= 12 && currentW < 15)
            {
                if (count <= 4000 && count % 30 == 0)
                {
                    CreateEnemy(randomlySelectedPrefabs[0]);
                    CreateEnemy(randomlySelectedPrefabs[1]);
                    CreateEnemy(randomlySelectedPrefabs[2]);
                }

                else if (count > 4000) { startChecking = true; }
            }

            else
            {
                // TODO Victory Screen
            }
        }
    }
}
