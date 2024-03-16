using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    public GameManager manager;
    public float moveSpeed;
    private Rigidbody rb;
    private Vector3 input;
    private float maxSpeed = 8f;
    private Vector3 spawn;
    public GameObject deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        // respawn player at specific position each time
        spawn = transform.position;
        // manager = GameObject.FindObjectOfType<GameManager>();        // initialize rigidbody component of player
        manager = manager.GetComponent<GameManager>();
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

    // To check object collision with enemy
    void OnCollisionEnter(Collision other){
        if (other.transform.tag == "Enemy"){
            Die();
        }
        
    }

    // To check object collision with portal (has "is trigger" enabled)
    void OnTriggerEnter(Collider other){
        if(other.transform.tag == "Enemy"){
            Die();
        }
        if (other.transform.tag == "Goal"){
            manager.CompleteLevel();
        }
        if(other.transform.tag == "Wisp"){
            manager.AddWisp();
            Destroy(other.gameObject);
            //TIP IF WE WANT TIMER DESTROY after 4sec, 
            // Destroy(other.gameObject, 4f);
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
