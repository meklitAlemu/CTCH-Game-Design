using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		// private Vector3 moveDirection = Vector3.zero;
		public float gravity = 4f;

    	public GameManager manager;
		private Vector3 spawn;
		public GameObject deathParticles;

		// //my edit
		public float jumpSpeed = 5f; // Jump speed
		private bool isJumping = false; // Track if player is jumping
		private float distToGround = 0.25f;
		private bool isGrounded = true; // Track grounded state
		private int currentMoveState = 0;


		Vector3 velocity;
		Vector3 move;


		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		
			// respawn player at specific position each time
			spawn = transform.position;
			// manager = GameObject.FindObjectOfType<GameManager>();        // initialize rigidbody component of player
			manager = manager.GetComponent<GameManager>();
			// // Access the collider through the CharacterController component
    		// distToGround = controller.bounds.extents.y;		
			}

		void Update (){
			HandleMovement();
			PlayerFlip();

		}
		void HandleMovement(){
			isGrounded = IsGrounded();

			if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
				anim.SetInteger ("AnimationPar", 1);
				currentMoveState = 1;
			}	
			else {
				anim.SetInteger ("AnimationPar", 0);
				currentMoveState = 0;
				
			}
   
			// Jumping
			if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
				isJumping = true;
				// anim.SetTrigger("takeOff"); // jump animations mess up stuff 
			}
			// Go up
			if (isJumping){
				// anim.SetBool("isJumping", isJumping);
				velocity.y = 0f; // Ensure no downward velocity when grounded
				isGrounded = false;
				velocity.y += jumpSpeed;
				controller.Move(velocity * Time.deltaTime);
				isJumping = false;
			}
			// Applying Gravity, go down
			if (!isGrounded) {
				// anim.SetBool("isJumping", isJumping);
				velocity.y -= gravity * Time.deltaTime;
				controller.Move(velocity * Time.deltaTime);
			} 
			// else if (isGrounded){
			// 	anim.SetInteger ("AnimationPar", currentMoveState);
			// }
	
			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");

			Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
			movementDirection.Normalize();

			// Player Rotation
			if(movementDirection != Vector3.zero){
				Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed*Time.deltaTime);
			}

			// Player movement
			controller.Move(movementDirection * speed * Time.deltaTime);

			// Check if below ground, then die
			if (transform.position.y < -2) {
				Die();
			}


		}

		void PlayerFlip(){
			if (Input.GetKey ("x")){
				anim.SetBool ("isFlipping", true);
			}
			else{
				anim.SetBool ("isFlipping", false);
			}
		}
		// To check object collision with enemy
		// void OnCollisionEnter(Collision other){
		// 	if (other.transform.tag == "Enemy"){
		// 		Die();
		// 	}
			
		// }
		void OnControllerColliderHit(ControllerColliderHit hit){
			if (hit.transform.CompareTag("Ground") && !isGrounded) {
				// print("Player Y Location: " + transform.position.y);
				isGrounded = true;
			}
			if (hit.transform.tag == "Enemy"){
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
			if (other.transform.tag == "HealthPotion"){
				print("this is when health gets restored!");
				Destroy(other.gameObject);
			}	
		}

		void Die(){
			// instantiate explotion with deathPartiles in prefab folder, at player's last position, rotation = 0
			// Quaternion.Euler(270, 0, 0)
			Instantiate(deathParticles, transform.position, Quaternion.identity);
			//player dead, set position to spaw point
			transform.position  = spawn;
		}

		// bool IsGrounded() {
		//     return isGrounded;
		// }

		bool IsGrounded() {
			// return Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, distToGround);
				return Physics.Raycast(transform.position + (Vector3.up * (distToGround + controller.height / 2)), Vector3.down, (distToGround + controller.height / 2 + 0.05f));

		}

	}
}
