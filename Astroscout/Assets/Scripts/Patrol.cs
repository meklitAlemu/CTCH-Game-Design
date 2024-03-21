using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    // Start is called before the first frame update
    private int currentPoint; //cooresponds to current patrol points
    public float moveSpeed;
    void Start()
    {
        // get patrol to start at the Partol point 1
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If patrol reaches one patrol points, turn around and walk to next point
        if (transform.position == patrolPoints[currentPoint].position){
            currentPoint++;
        }

        // return back to start point
        if (currentPoint >= patrolPoints.Length){
            currentPoint = 0;
        }
        // Take current position and move it towards another point
        // normally, the position is updated on frame rate but we chose to update it by delta time
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}
