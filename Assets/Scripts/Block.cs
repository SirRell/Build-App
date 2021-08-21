using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Material _material; //The blocks base material
    public MoveDirection MoveDirection { get; set; } //Moving on Z or X axis
    protected GameManager gManager; //Static game manager

    private void Awake()
    {
        gManager = GameManager.gameManager; //Assign the game manager
        if (gManager.LastCube == null) //If this is the first cube
            gManager.SetLastCube(GameObject.FindWithTag("Base"));//Set the base to be the starting cube
        gManager.SetCurrentCube(gameObject); //Set this block as the current block
        
        //Set the scale to the last cube, since they should be getting smaller
        transform.localScale = new Vector3(gManager.LastCube.transform.localScale.x, 
                                            transform.localScale.y,
                                            gManager.LastCube.transform.localScale.z);
    }

    void Start()
    {
        //Assign the block's renderer
        _material = gameObject.GetComponent<Renderer>().material;
        //Change the block's color to something random for variation
        if(gameObject.tag != "Base")
            _material.color = Random.ColorHSV();
    }

    //How much is the block hanging over the edge of the block below it
    protected float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - gManager.LastCube.transform.position.z;
        else
            return transform.position.x - gManager.LastCube.transform.position.x;
    }

    //Split the cube if there is hangover
    protected void SplitCubeOnX(float hangover, float direction)
    {
        //Find the size this block should be after it's "sliced"
        float newXSize = gManager.LastCube.transform.localScale.x - Mathf.Abs(hangover);
        //Find the size of the "sliced off" block
        float fallingBlockSize = transform.localScale.x - newXSize;

        //Find the edge of the previous block
        float newXPosition = gManager.LastCube.transform.position.x + (hangover / 2f);
        //Set the new scale of this block
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        //Move this block over to be flush with the edge
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        //Find the edge of this cube
        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        //Find where the sliced off block needs to be placed on the X axis
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;
        
        //Spawn a cube to drop at the right position and the right size
        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    protected void SplitCubeOnZ(float hangover, float direction)
    {
        //Find the size this block should be after it's "sliced"
        float newZSize = gManager.LastCube.transform.localScale.z - Mathf.Abs(hangover);
        //Find the size of the "sliced off" block
        float fallingBlockSize = transform.localScale.z - newZSize;

        //Find the edge of the previous block
        float newZPosition = gManager.LastCube.transform.position.z + (hangover / 2f);
        //Set the new scale of this block
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        //Move this block over to be flush with the edge
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        //Find the edge of this cube
        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        //Find where the sliced off block needs to be placed on the X axis
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;

        //Spawn a cube to drop at the right position and the right size
        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        //The falling block will be, of course, a plain ol' block
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //Apply the scale and position previously found, and that is passed into this function
        if (MoveDirection == MoveDirection.Z)
        {

            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }

        //Add a rigidbody, which allows the block to have gravity affect it
        cube.AddComponent<Rigidbody>();
        //Match the block's color
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        
        //Remove the block from existence after 1 second
        //TODO: Fade the cube out of existence
        Destroy(cube, 1f);
    }

}
