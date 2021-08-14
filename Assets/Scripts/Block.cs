using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Material _material;
    Rigidbody rb;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Get the block's renderer
        _material = gameObject.GetComponent<Renderer>().material;
        //Change the block's color to something random for variation
        _material.color = Random.ColorHSV();
        //Assign the ridgidbody
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    public void CheckBlockPosition()
    {
        Vector3 offset = transform.localScale / 2;
        Vector3 origin = transform.position + new Vector3(offset.x, 0, offset.z);
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(origin, Vector3.down, Color.red, 10);

        if(Physics.Raycast(ray, out hit))
        {
            print(hit.transform.name);
            Placed();
        }
        else
        {
            Missed();
        }
    }

    void Placed()
    {
        SpawnNewBlock();
    }

    void SpawnNewBlock()
    {
        BlockMovement bm = gameObject.GetComponent<BlockMovement>();
        GameObject newBlock = Instantiate(gameObject, new Vector3(-bm.startPos.x, bm.startPos.y + transform.localScale.y, -bm.startPos.z), Quaternion.identity);
        newBlock.name = "BuildingBlock";
        Destroy(bm);

        //Let camera know it's time to move up
        cam.GetComponent<CameraMovement>().SetNewPosition();
    }

    void Missed()
    {

    }
}
