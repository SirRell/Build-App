using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;
    [SerializeField]
    MoveDirection moveDirection;

    public void SpawnCube()
    {

        GameObject block = Instantiate(blockPrefab);

        if (GameManager.gameManager.LastCube != null && GameManager.gameManager.LastCube.gameObject != GameObject.FindWithTag("Base"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : GameManager.gameManager.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : GameManager.gameManager.LastCube.transform.position.z;
            block.transform.position = new Vector3(x,
                    GameManager.gameManager.LastCube.transform.position.y + block.transform.localScale.y,
                    z);
        }
        else
        {
            block.transform.position = transform.position;
        }

        block.GetComponent<BlockMovement>().MoveDirection = moveDirection;
    }
}
