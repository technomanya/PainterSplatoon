
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Project.Runtime.Scripts.Managers;
using UnityEngine;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

	public Joystick joystick;
	public float slopeConstant;
	private Vector3 _inputVector, _movementVector;
	
    public float Velocity;
    [Space]

	private Animator anim;
	private Camera cam;
	private CharacterController controller;
	private bool isGrounded;
	private Vector3 desiredMoveDirection;
	private float InputX;
	private float InputZ;

	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;

	public float speed;
	public float sensitivity;
	public float allowPlayerRotation = 0.1f;


    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

	void Start () {
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
	}
	
	void Update () {

		if(!Input.GetMouseButton(0))
			return;
		
			
		InputMagnitude ();

        isGrounded = controller.isGrounded;
        if (isGrounded)
            verticalVel -= 0;
        else
            verticalVel -= 1;

        moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        
		controller.Move(moveVector);
    }
	
	private void GetPlayerInput()
	{
		_inputVector.x = -joystick.Horizontal;
		_inputVector.z = -joystick.Vertical;
        
	}

	private void UpdateDynamicMovementSpeed()
	{
		if ((Vector3.Distance(_movementVector, _inputVector) > 0.1f))
			_movementVector += (_inputVector - _movementVector).normalized * Time.deltaTime * slopeConstant;
		else _movementVector = _inputVector;
	}

	void PlayerMoveAndRotation() {
		/*InputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");*/

		/*InputX = joystick.Horizontal;
		InputZ = joystick.Vertical;*/

		InputX = Input.GetAxis("Mouse X");
		InputZ = Input.GetAxis("Mouse Y");

		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();

		desiredMoveDirection = forward * InputZ + right * InputX;

		if (blockRotationPlayer == false) {
			//Camera
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
			controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);
		}
		else
		{
			//Strafe
			controller.Move((transform.forward * InputZ + transform.right  * InputX) * Time.deltaTime * Velocity);
		}
	}

    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {
        var forward = cam.transform.forward;

        desiredMoveDirection = forward;
		Quaternion lookAtRotation = Quaternion.LookRotation(desiredMoveDirection);
		Quaternion lookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		t.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationOnly_Y, desiredRotationSpeed);
	}

	void InputMagnitude() {
		//Calculate Input Vectors
		/*InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");*/
		
		InputX = Input.GetAxis("Mouse X");
		InputZ = Input.GetAxis("Mouse Y");

		//Calculate the Input Magnitude
		speed = new Vector2(InputX, InputZ).sqrMagnitude;

		//Change animation mode if rotation is blocked
		anim.SetBool("shooting", blockRotationPlayer);

		//Physically move player
		if (speed > allowPlayerRotation) {
			anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
			anim.SetFloat("X", InputX, StartAnimTime/3, Time.deltaTime);
			anim.SetFloat("Y", InputZ, StartAnimTime/3, Time.deltaTime);
			PlayerMoveAndRotation ();
		} else if (speed < allowPlayerRotation) {
			anim.SetFloat ("Blend", speed, StopAnimTime, Time.deltaTime);
			anim.SetFloat("X", InputX, StopAnimTime/ 3, Time.deltaTime);
			anim.SetFloat("Y", InputZ, StopAnimTime/ 3, Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Coin"))
		{
			other.transform.parent.DOScale(Vector3.one * 2, 1f);
			GameManager.instance.levelManager.currentLevel.coins.Remove(other.gameObject);
			GameManager.instance.score += 10;
			Destroy(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Coin"))
		{
			//other.transform.localScale;
		}
	}
}
