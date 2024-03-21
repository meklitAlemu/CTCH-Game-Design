using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    // Start is called before the first frame update
    private int currentPoint; //cooresponds to current patrol points
    public float moveSpeed;
	public float turnSpeed = 400.0f;
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
        // Calculate the direction to the next patrol point
        Vector3 directionToNextPoint = patrolPoints[currentPoint].position - transform.position;
        directionToNextPoint.y = 0f; // Ignore vertical component for 2D movement

        // Rotate the enemy towards the next patrol point
        if (directionToNextPoint != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(directionToNextPoint, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        // Move the enemy towards the next patrol point
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
    }
}
