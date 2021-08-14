using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 targetLocation = new Vector3(-2.5f, 1, -2.5f);
    // Start is called before the first frame update
    void Start()
    {
        
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
