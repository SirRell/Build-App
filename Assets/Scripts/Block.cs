using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Material _material;
    public MoveDirection MoveDirection { get; set; }
    protected GameManager gManager;

    private void Awake()
    {
        gManager = GameManager.gameManager;
        if (gManager.LastCube == null)
            gManager.SetLastCube(GameObject.FindWithTag("Base"));//Will be the starting cube
        gManager.SetCurrentCube(gameObject);
        
        transform.localScale = new Vector3(gManager.LastCube.transform.localScale.x, transform.localScale.y, gManager.LastCube.transform.localScale.z);
    }

    void Start()
    {
        //Get the block's renderer
        _material = gameObject.GetComponent<Renderer>().material;
        //Change the block's color to something random for variation
        if(gameObject.tag != "Base")
            _material.color = Random.ColorHSV();


        
    }

    protected float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - gManager.LastCube.transform.position.z;
        else
            return transform.position.x - gManager.LastCube.transform.position.x;
    }

    protected void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = gManager.LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = gManager.LastCube.transform.position.x + (hangover / 2f);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    protected void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = gManager.LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = gManager.LastCube.transform.position.z + (hangover / 2f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;



        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

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

        cube.AddComponent<Rigidbody>();
        //Match the block's color
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        //TODO: Fade the cube out of existence
        Destroy(cube, 1f);
    }

}
