using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockMovement : Block
{
    [SerializeField] float speed = 3f;
    private float timeElapsed = 0;
    //float traveledDistance;
    float fracJourney;
    private Vector3 startPos;

   

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (speed > 0)
        {
            //Increment how much time has passed
            timeElapsed += Time.deltaTime;
            fracJourney = Mathf.PingPong(timeElapsed / speed, 1);
           
            //Move the block back and forth
            if (MoveDirection == MoveDirection.Z)
                transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, startPos.y, -startPos.z), fracJourney);
            else
                transform.position = Vector3.Lerp(startPos, new Vector3(-startPos.x, startPos.y, startPos.z), fracJourney);
        }
        
    }

    internal void Stop()
    {
        speed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? gManager.LastCube.transform.localScale.z : gManager.LastCube.transform.localScale.x;
        if (Mathf.Abs(hangover) >= max)
        {
            gManager.SetLastCube(null);
            gManager.SetCurrentCube(null);
            GameManager.gameManager.DisplayLost();
            return;
        }
        if (Mathf.Abs(hangover) > .15f)
        {
            float direction = hangover > 0 ? 1f : -1f;

            if (MoveDirection == MoveDirection.Z)
                SplitCubeOnZ(hangover, direction);
            else
                SplitCubeOnX(hangover, direction);
        }
        else
        {
            transform.position = gManager.LastCube.transform.position;
            //Reposition on the Y axis
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            print("Good Placement!");
        }

        gManager.SetLastCube(gameObject);
        //Destroy(this);//Remove this script from the gameObject
    }

}
