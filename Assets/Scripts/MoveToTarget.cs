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


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingWaypoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(targetWaypoint.transform.position, transform.position) <= accuracy)
        {
            //Stop at Target
            transform.position = targetWaypoint.transform.position;
            movementSpeed = 0;
            rotationSpeed = 0;
        }

        //Rotate towards target
        Vector3 direction = targetWaypoint.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        // Move in the front dirction
        transform.Translate(0, 0, Time.deltaTime * movementSpeed);

        
    }

   

 

    /*public float calculateDistanceVector(Vector3 A, Vector3 B, Vector3 P)
         {
             //Calculate Vector V
             Vector3 V = P;
             V.x -= A.x;
             V.y -= A.y;
             V.z -= A.z;

             //Calculate Vector U
             Vector3 U = B;
             U.x -= A.x;
             U.y -= A.y;
             U.z -= A.z;

             //Calculate Vector VU
             float VU_DOT = Vector3.Dot(V, U);
             float U_MAG_SQRD = U.magnitude * U.magnitude;
             Vector3 VU = VU_DOT * (U / U_MAG_SQRD);

             //Calculate Vector V_PERP
             Vector3 V_PERP = V - VU;

             return V_PERP.magnitude;
         }*/
}
