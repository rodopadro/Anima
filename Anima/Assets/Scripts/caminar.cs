using UnityEngine;
using System.Collections;

public class caminar : MonoBehaviour {

    public int speed;
    private bool moving;
    private bool sprinting;
    private int movingConstant; //cuando camina = 1, cuando corre = 2
    private bool sprintCoroutineActive;
    private float verticalAxis, horizontalAxis;
	void Start () {
        moving = sprinting = sprintCoroutineActive = false;
        movingConstant = 1;
	}
	
	// Update is called once per frame
	void Update () {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        if (verticalAxis != 0.0f || horizontalAxis != 0.0f)
        {
            moving = true;
            StartCoroutine("waitingForTheRun");
        }
        else if(verticalAxis == 0.0f && horizontalAxis == 0 && moving)
        {
            moving = false;
           
            
            if (sprintCoroutineActive)
            {
                StopCoroutine("waitingForTheRun");
            }
            movingConstant = 1;
        }

        if (moving)
        {
            transform.Translate(new Vector3(horizontalAxis, 0, verticalAxis) * speed * movingConstant * Time.deltaTime);
            print("rolling around at the speed of sound: " + movingConstant);
        }
	}
    IEnumerator waitingForTheRun()
    {
        while (moving)
        {
            sprintCoroutineActive = true;
            yield return new WaitForSeconds(1.5f);
            movingConstant = 2;
            sprintCoroutineActive = false;
        }
        
    }
}
