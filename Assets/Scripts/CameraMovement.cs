using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 targetLocation;
    void Start()
    {
        targetLocation = transform.position;
        GameManager.OnCubeSpawned += SetNewPosition;
    }
    public void SetNewPosition()
    {
        targetLocation += Vector3.up * .2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime);
    }
}
