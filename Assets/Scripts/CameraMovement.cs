using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 targetLocation; //Where the camera should move to
    void Start()
    {
        //Set the spawn location as the target location
        targetLocation = transform.position;
        //When the GameManager says a new cube has been spawned, change positions
        GameManager.OnCubeSpawned += SetNewPosition;
    }
    public void SetNewPosition()
    {
        //Set the target position up .2 units, which is the same scale as the cube's Y axis
        targetLocation += Vector3.up * .2f;
        //Vector3.up is just (0, 1, 0)
    }

    // Update is called once per frame
    void Update()
    {
        //Continuously move the camera toward the target position smoothly
        transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime);
    }
}
