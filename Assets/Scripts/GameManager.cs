using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static event Action OnCubeSpawned = delegate { };

    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    public GameObject CurrentCube { get; private set; }
    public GameObject LastCube { get; private set; }

    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        else
            Destroy(gameObject);

        spawners = FindObjectsOfType<CubeSpawner>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (CurrentCube != null && CurrentCube != GameObject.FindWithTag("Base"))
                CurrentCube.GetComponent<BlockMovement>().Stop();
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            currentSpawner.SpawnCube();
            OnCubeSpawned(); //Used to update the score text
        }
    }

    public void SetCurrentCube(GameObject newCube)
    {
        CurrentCube = newCube;
    }

    public void SetLastCube(GameObject newCube)
    {
        LastCube = newCube;
    }

    //Display Lost
    public void DisplayLost()
    {
        SceneManager.LoadScene(0);
    }
}
