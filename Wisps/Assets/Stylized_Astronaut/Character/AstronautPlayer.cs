using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

    	public GameManager manager;
		private Vector3 spawn;
		public GameObject deathParticles;


		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		
			// respawn player at specific position each time
			spawn = transform.position;
			// manager = GameObject.FindObjectOfType<GameManager>();        // initialize rigidbody component of player
			manager = manager.GetComponent<GameManager>();
		}

		void Update (){
			if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
				anim.SetInteger ("AnimationPar", 1);
			}
			else {
				anim.SetInteger ("AnimationPar", 0);
			}

			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");

			Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
			movementDirection.Normalize();
			transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

			if(movementDirection != Vector3.zero){
				Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed*Time.deltaTime);
			}

			if(transform.position.y <  -2){
				Die();
			}
				
			//old code
			// if(controller.isGrounded){
			// 	moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			// }

			// float turn = Input.GetAxis("Horizontal");
			// transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			// controller.Move(moveDirection * Time.deltaTime);
			// moveDirection.y -= gravity * Time.deltaTime;
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
}
