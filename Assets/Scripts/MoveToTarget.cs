using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public GameObject startingWaypoint;
    public GameObject targetWaypoint;
    
    public float movementSpeed = 4;
    public int rotationSpeed = 2;

    //how close to get to the waypoint to consider having reached it
    public float accuracy = 5.0f;

    // Update is called once per frame
    void Update()
    {
        //Rotate towards target
        Vector3 direction = targetWaypoint.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        // Move in the front dirction
        transform.Translate(0, 0, Time.deltaTime * movementSpeed);        
    }

}
