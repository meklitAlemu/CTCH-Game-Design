using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  

    public float moveSpeed;
    private Rigidbody rb;
    private Vector3 input;
    private float maxSpeed = 5f;
    private Vector3 spawn;
    public GameObject deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        // respawn player at specific position each time
        spawn = transform.position;
        // initialize rigidbody component of player
        rb = GetComponent<Rigidbody>();
    }

    // Update is called 50X/sec. At fixed rate
    void FixedUpdate()
    {        
        // input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //No smoothing to movement applied with GetAxisRaw
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        // Edit for quicker movement / acceleration at start of movement
        if(rb.velocity.magnitude < maxSpeed){
            // Think of add force like push (like push )
            // rb.AddForce(input * moveSpeed);    
            rb.AddRelativeForce(input * moveSpeed);    
        }
        // If fall off map, die
        if(transform.position.y <  -2){
            Die();
        }
    }

    void OnCollisionEnter(Collision other){
        if (other.transform.tag == "Enemy"){
            Die();
        }
        
    }

    void OnTriggerEnter(Collider other){
        if (other.transform.tag == "Goal"){
            GameManager.CompleteLevel();
        }
    }

    void Die(){
        // instantiate explotion with deathPartiles in prefab folder, at player's last position, rotation = 0
        // Quaternion.Euler(270, 0, 0)
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        //player dead, set position to spaw point
        transform.position  = spawn;
    }
}
