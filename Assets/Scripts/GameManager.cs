using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; //Static Game Manager, so other objects/scripts in the scene can easily find and reference it
    public static event Action OnCubeSpawned = delegate { }; //Announcement sent out that a cube has been spawned

    private CubeSpawner[] spawners; //Array of the two cube-spawners
    private int spawnerIndex; //Current index of the spawners to use
    private CubeSpawner currentSpawner; //The current spawner that will spawn a cube

    public GameObject CurrentCube { get; private set; } //Current cube being placed
    public GameObject LastCube { get; private set; } //Last cube placed 

    private void Awake()
    {
        //If there is no game manager, set this to be it
        if (gameManager == null)
            gameManager = this;
        else
            Destroy(gameObject);

        //Find and assign the game objects that will spawn cubes
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    void Update()
    {
        //If the player clicks the mouse, touches the mobile screen, or presses the space key on keyboard
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            //If there is a cube to place, and it is not the Base block
            if (CurrentCube != null && CurrentCube != GameObject.FindWithTag("Base"))
                //Stop the cube from moving and place it where it is
                CurrentCube.GetComponent<BlockMovement>().Stop();
            //Change the spawner to use
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            //Make the current spawner game object spawn a cube
            currentSpawner.SpawnCube();
            //Announce that a cube has been spawned.
            OnCubeSpawned(); //Right now, it's just used to update the score text
        }
    }

    public void SetCurrentCube(GameObject newCube)
    {
        //Set the Current Cube to the game object that is passed to this function
        CurrentCube = newCube;
    }

    public void SetLastCube(GameObject newCube)
    {
        //Set the Last Cube to the game object that is passed to this function
        LastCube = newCube;
    }

    //Display Lost
    public void DisplayLost()
    {
        /*Right now all thius function does is reload the scene
          TODO: Change this functionality to show the end score, expand the camera's view to show
          how tall the player made the stack*/
        SceneManager.LoadScene(0);
    }
}
