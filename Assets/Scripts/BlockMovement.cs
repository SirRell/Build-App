using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float speed = 0;
    [HideInInspector] public Vector3 startPos;
    float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Assign where the block is spawning at
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (speed > 0)
        {
            //Increment how much time has passed
            timeElapsed += Time.deltaTime;
            //Move the block back and forth
            transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x * -1, 0, startPos.z * -1), Mathf.PingPong(timeElapsed / speed, 1));
            //Set Y position to original location, because it likes to float away for some reason
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<Block>().CheckBlockPosition();
        }

    }
}
