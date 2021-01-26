using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    //Simulation Variables
    public static bool SimulationPaused = false;
    public static bool SimulationReset = false;
    public static bool SimulationFinished = false;
    private float SimulationRunTime;
    private float SimulationDistance;
    public Text DeltaTime;
    public Text DeltaDistance;
    public Text PausePlay;
    public Text RedSpeed;
    public Text BlueSpeed;
    public Text Outcome;
    public Text Beta;
    public Text Alpha;

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
        BlueSpeed.text = PreyMovementSpeed.ToString();
        RedSpeed.text = PredatorMovementSpeed.ToString();
        
        Reset();
    }

    // Update is called once per frame
    void Update() 
    {
        SimulationRunTime += Time.deltaTime;
        DeltaTime.text = SimulationRunTime.ToString();
        SimulationDistance = calculateDistance(Prey.transform.position, Predator.transform.position);
        DeltaDistance.text = SimulationDistance.ToString();

       
        if (SimulationDistance <= BetaDistance)
        {
            //Stop and Fire Laser
            Prey.GetComponent<MoveToTarget>().movementSpeed = 0f;
            Prey.GetComponent<MoveToTarget>().rotationSpeed = 0;
            Predator.GetComponent<MoveToTarget>().movementSpeed = 0f;
            Predator.GetComponent<MoveToTarget>().rotationSpeed = 0;
            SimulationPaused = true;
            SimulationFinished = true;
            PausePlay.text = "Retry Simulation";
            Time.timeScale = 0f;
            Outcome.text = "Red Wins!";

        }

        if(calculateDistance(Prey.transform.position, PreyDestinationWaypoint.transform.position) < BetaDistance && Vector3.Angle(Predator.transform.position, Prey.transform.position) < AlphaAngle)

        {
            //Stop and Fire Laser
            Prey.GetComponent<MoveToTarget>().movementSpeed = 0f;
            Prey.GetComponent<MoveToTarget>().rotationSpeed = 0;
            Predator.GetComponent<MoveToTarget>().movementSpeed = 0f;
            Predator.GetComponent<MoveToTarget>().rotationSpeed = 0;
            SimulationPaused = true;
            SimulationFinished = true;
            PausePlay.text = "Retry Simulation";
            Time.timeScale = 0f;
            Outcome.text = "Blue Wins!";

        }
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
            if(SimulationFinished == true)
            {
                SimulationFinished = false;
                Reset();
            }
            else
            {
                
               PreyMovementSpeed = float.Parse(BlueSpeed.text);
               PredatorMovementSpeed = float.Parse(RedSpeed.text);
               BetaDistance = float.Parse(Beta.text);
               AlphaAngle = float.Parse(Alpha.text);
               Prey.GetComponent<MoveToTarget>().movementSpeed = PreyMovementSpeed;
               Predator.GetComponent<MoveToTarget>().movementSpeed = PredatorMovementSpeed;
               Resume();
               PausePlay.text = "Pause Simulation";
               Outcome.text = "In Progress...";

            }

                 
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

        SimulationDistance = calculateDistance(Prey.transform.position, Predator.transform.position);
        DeltaDistance.text = SimulationDistance.ToString();
        SimulationRunTime = 0f;
        DeltaTime.text = SimulationRunTime.ToString();
        PausePlay.text = "Start Simulation";
        Outcome.text = "";

    }
}
