using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform currentObject;
    public Agent currentAgent;
    public bool wasFixedOnTheAgent = false;
    public GameController gameController;
    private Vector3 screenMousePosition;
    public float speed = 5.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (transform.parent != null)
        {
            if (gameController.isPlay == true)
            {
                wasFixedOnTheAgent = true;
                currentObject = transform.parent;
                currentAgent = currentObject.GetComponent<Agent>();
                //Debug.Log("Direction ----------------- " + currentAgent.agent_directions.ElementAt(gameController.count_frame));
                if (currentAgent.frames.Contains((gameController.count_frame + 20).ToString()) && currentAgent.agentActivatedOnTheScene == true && currentAgent.frames.Contains((gameController.count_frame + 1).ToString()))
                {
                    Vector3 relativePos2 = currentAgent.movements.ElementAt(currentAgent.frames.IndexOf((gameController.count_frame).ToString()));//Vector3 relativePos2 = movements.ElementAt(frames.IndexOf((gameController.count_frame + 10).ToString())) - movements.ElementAt(frames.IndexOf(gameController.count_frame.ToString()));

                    for (int i = 1; i <= 5; i++)
                    {
                        relativePos2 = relativePos2 + currentAgent.movements.ElementAt(currentAgent.frames.IndexOf((gameController.count_frame + i).ToString()));
                    }
                    
                    //Vector3 relativePos = currentAgent.movements.ElementAt(currentAgent.frames.IndexOf((gameController.count_frame + 1).ToString())) - currentAgent.movements.ElementAt(currentAgent.frames.IndexOf(gameController.count_frame.ToString()));
                    Quaternion rotationAgent = Quaternion.LookRotation((relativePos2/5) - currentAgent.movements.ElementAt(currentAgent.frames.IndexOf((gameController.count_frame).ToString())), Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotationAgent, Mathf.SmoothStep(0.0f, 1.5f, Time.deltaTime));

                }
                else
                {
                    if (currentAgent.frames.Contains(gameController.count_frame.ToString()))
                    {
                        Vector3 relativePos = currentAgent.movements.ElementAt(currentAgent.frames.IndexOf(currentAgent.frames.Count.ToString())) - currentAgent.movements.ElementAt(currentAgent.frames.IndexOf(gameController.count_frame.ToString()));
                        Quaternion rotationAgent = Quaternion.LookRotation(relativePos, Vector3.up);
                        transform.rotation = Quaternion.Lerp(transform.rotation, rotationAgent, Mathf.SmoothStep(0.0f, 1.5f, Time.deltaTime));
                    }
                }

            }

            else
            {
                screenMousePosition.x = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
                screenMousePosition.y = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
                Quaternion rotationAgent = Quaternion.LookRotation(new Vector3(screenMousePosition.x, 0, 0));
                if (screenMousePosition.x < 1 && screenMousePosition.x >= 0.87 && screenMousePosition.y <= 0.7562 && screenMousePosition.y >= 0.2052)
                {
                    //Debug.Log(screenMousePosition.x + "------------------------- direitoooooooooooo");

                    rotationAgent.w = -rotationAgent.w;

                    //Debug.Log(rotationAgent.w);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotationAgent, Time.deltaTime * 0.5f);
                }
                if (screenMousePosition.x >= 0 && screenMousePosition.x <= 0.2 && screenMousePosition.y <= 0.7562 && screenMousePosition.y >= 0.2052)
                {

                    //Debug.Log(screenMousePosition.x + "------------------------- esquerdoooooooooo ");

                    //rotationAgent.x = -rotationAgent.x;
                    // Debug.Log(-rotationAgent.w);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotationAgent, Time.deltaTime * 0.5f);
                }
                //Debug.Log("X - " + Camera.main.ScreenToViewportPoint(Input.mousePosition).x + " Y - " + Camera.main.ScreenToViewportPoint(Input.mousePosition).y );
                //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition).x <= 1 && Camera.main.ScreenToViewportPoint(Input.mousePosition).x >= 0 && Camera.main.ScreenToViewportPoint(Input.mousePosition).y <= 0.2 && Camera.main.ScreenToViewportPoint(Input.mousePosition).y >= 0);
            }
            
            
        }
        else
        {
            wasFixedOnTheAgent = false;
            currentAgent = null;
            currentObject = null;
            //gameController.ResetPositionCamera();


            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }

        }
	}
}
