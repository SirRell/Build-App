using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockMovement : Block
{
    [SerializeField] float speed = 3f;//How fast the block will move
    float timeElapsed = 0; //Used for the blocks lerp movement
    float fracJourney; //Used for the blocks lerp movement
    private Vector3 startPos; //Where the cube spawned in the world

   

    private void Start()
    {
        //Set the start position
        startPos = transform.position;
    }

    void Update()
    {
        //If the block is moving
        if (speed > 0)
        {
            //Increment how much time has passed
            timeElapsed += Time.deltaTime;
            //Calculate the fraction, so it's a decimal from 0 - 1
            fracJourney = Mathf.PingPong(timeElapsed / speed, 1);
           
            //Move the block back and forth smoothly
            if (MoveDirection == MoveDirection.Z)
                transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, startPos.y, -startPos.z), fracJourney);
            else
                transform.position = Vector3.Lerp(startPos, new Vector3(-startPos.x, startPos.y, startPos.z), fracJourney);
        }
        
    }

    internal void Stop() //Stop the block
    {
        //Set movement speed to 0
        speed = 0;
        //Find how much the cube is hanging over the edge of the block below it
        float hangover = GetHangover();

        //Find how much the block can be over, without being completely over the edge
        float max = MoveDirection == MoveDirection.Z ? gManager.LastCube.transform.localScale.z : gManager.LastCube.transform.localScale.x;
        //If the MoveDirection is Z, then the scale on the Z axis will be used - else, use the X scale

        //If the block would fall and the player would lose
        if (Mathf.Abs(hangover) >= max)
        {
            //Set the game manager's last and current cube to be nothing, so there isn't a "null reference error"
            gManager.SetLastCube(null);
            gManager.SetCurrentCube(null);

            //Show the player has lost the game
            GameManager.gameManager.DisplayLost();
            //Return out of this function, without doing anything past this point
            return;
        }
        //If the player isn't close to perfect
        if (Mathf.Abs(hangover) > .15f)
        {
            //Find with direction the cube should be "sliced" in
            float direction = hangover > 0 ? 1f : -1f;

            //"Slice" the cube on the correct axis
            if (MoveDirection == MoveDirection.Z)
                SplitCubeOnZ(hangover, direction);
            else
                SplitCubeOnX(hangover, direction);
        }
        else //If the player got close enough to perfect
        {
            //Put this block in the same position of the previous block
            transform.position = gManager.LastCube.transform.position;
            //Reposition on the Y axis
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
        //Make this block the Last Cube, because another cube has been spawned
        gManager.SetLastCube(gameObject);
    }

}
