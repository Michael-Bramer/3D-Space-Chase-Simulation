using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    //Simulation Variables
    public static bool SimulationPaused = false;
    public static bool SimulationReset = false;
    private float SimulationRunTime;
    public Text DeltaTime;
    public Text DeltaDistance;
    public Text PausePlay;

    //Representational Objects
    public GameObject Prey;
    public GameObject Predator;

    //Movement Variables
    public float PreyMovementSpeed = 4;
    public int   PreyRotationSpeed = 2;
    public float PredatorMovementSpeed = 4;
    public int   PredatorRotationSpeed = 2;
    
    //Pedator Line of Fire Variables
    public float AlphaAngle = 45;
    public float BetaDistance = 1;

    //Destination Objects
    public GameObject PreyDestinationWaypoint;
    public GameObject PreyStartingWaypoint;
    public GameObject PredatorStartingWaypoint;

    //Other Metrics
    private float distanceFromTarget = 0;

    // Start is called before the first frame update
    void Start()
    {   
        //Reset the Simulation
        Reset();
    }

    // Update is called once per frame
    void Update() 
    {
        SimulationRunTime += Time.deltaTime;
        DeltaDistance.text = calculateDistance(Prey.transform.position, Predator.transform.position).ToString();
        DeltaTime.text = SimulationRunTime.ToString();

        /*
        if (Vector3.Distance(targetWaypoint.transform.position, transform.position) <= accuracy)
        {
            //Stop at Target
            transform.position = targetWaypoint.transform.position;
            movementSpeed = 0;
            rotationSpeed = 0;
        }
        */
    }

    //Calculate the Distance Between 2 Vectors
    public float calculateDistance(Vector3 baseObject, Vector3 targetObject)
    {
        float deltaX = (baseObject.x - targetObject.x);
        float deltaY = (baseObject.y - targetObject.y);
        float deltaZ = (baseObject.z - targetObject.z);

        return Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY) + (deltaZ * deltaZ));
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SimulationPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        SimulationPaused = true;
    }

    public void AlternatePauseResume()
    {
        if(SimulationPaused == true)
        {
            Resume();
            PausePlay.text = "Pause Simulation";
        }
        else
        {
            Pause();
            PausePlay.text = "Resume Simulation";
        }
    }

    public void Reset()
    {
        //Pause the Simulation
        Pause();

        //Reset Object Attributes
        Prey.GetComponent<Transform>().position            = PreyStartingWaypoint.GetComponent<Transform>().position;
        Prey.GetComponent<MoveToTarget>().movementSpeed    = PreyMovementSpeed;
        Prey.GetComponent<MoveToTarget>().rotationSpeed    = PreyRotationSpeed;
        Prey.GetComponent<MoveToTarget>().startingWaypoint = PreyStartingWaypoint;
        Prey.GetComponent<MoveToTarget>().targetWaypoint   = PreyDestinationWaypoint;

        Predator.GetComponent<Transform>().position            = PredatorStartingWaypoint.GetComponent<Transform>().position;
        Predator.GetComponent<MoveToTarget>().movementSpeed    = PredatorMovementSpeed;
        Predator.GetComponent<MoveToTarget>().rotationSpeed    = PredatorRotationSpeed;
        Predator.GetComponent<MoveToTarget>().startingWaypoint = PredatorStartingWaypoint;
        Predator.GetComponent<MoveToTarget>().targetWaypoint   = Prey;

        DeltaDistance.text = calculateDistance(Prey.transform.position, Predator.transform.position).ToString();
        SimulationRunTime = 0f;
        DeltaTime.text = SimulationRunTime.ToString();
        PausePlay.text = "Start Simulation";

    }
}
