using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab; //The prefab to be spawned
    [SerializeField]
    MoveDirection moveDirection; //The axis the cube is moving on

    public void SpawnCube()
    {
        //Assign the newly created object to the variable so we can modify the prefab
        GameObject block = Instantiate(blockPrefab);

        //If there is a cube underneath, and it isn't the base cube, spawn a cube at the correct Y position
        if (GameManager.gameManager.LastCube != null && GameManager.gameManager.LastCube.gameObject != GameObject.FindWithTag("Base"))
        {
            //Find the correct X position
            //If the block is moving on the X axis, keep this x position - else use the block underneath's X position
            float x = moveDirection == MoveDirection.X ? 
                transform.position.x : GameManager.gameManager.LastCube.transform.position.x;
            //If the block is moving on the X axis, keep this x position - else use the block underneath's Z position
            float z = moveDirection == MoveDirection.Z ?
                transform.position.z : GameManager.gameManager.LastCube.transform.position.z;

            //Move the block into the correct position
            block.transform.position = new Vector3(x,
                    GameManager.gameManager.LastCube.transform.position.y + block.transform.localScale.y,
                        z);
        }
        else //This is the first block spawned 
        {
            //Spawn it at the location this Spawner is located
            block.transform.position = transform.position;
        }

        //Set the spawned block's move direction to be this spawner's assigned move direction (Set in the inspector)
        block.GetComponent<BlockMovement>().MoveDirection = moveDirection;
    }
}
