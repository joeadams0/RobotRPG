using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float cameraHeight;
	public float cameraDistance;
	public Transform cameraTransform;
	
	public float walkSpeed;
	public float runSpeed;
	
	public AnimationClip idleClip;
	public AnimationClip forwardClip;
	public AnimationClip backwardClip;
	public AnimationClip leftClip;
	public AnimationClip rightClip;
	
	Vector3 forwardDir;
	Vector3 cameraForwardDir;
	
	Animation anim;
	
	bool running;
	
	enum MoveDir
	{
		Idle, Forward, Backward, Left, Right
	};
	
	// Use this for initialization
	void Start () 
	{
		cameraForwardDir = cameraTransform.forward;
		cameraForwardDir.y = 0;
		cameraForwardDir = cameraForwardDir.normalized;
		
		updateCameraPosition();
		Vector3 forward = transform.position - cameraTransform.position;
		cameraTransform.forward = forward;
		
		anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rotateTowardMouse();
		
		CharacterController control = (CharacterController)GetComponent<CharacterController>();
		
		if (Input.GetKey(KeyCode.LeftShift))
		{
			running = true;
		}
		else
		{
			running = false;
		}
		
		MoveDir verticalDir = MoveDir.Idle;
		MoveDir horizontalDir = MoveDir.Idle;
		
		if (Input.GetKey(KeyCode.W))
		{
			if (running)
				control.SimpleMove(cameraForwardDir*Time.deltaTime*runSpeed);
			else
				control.SimpleMove(cameraForwardDir*Time.deltaTime*walkSpeed);
			verticalDir = MoveDir.Forward;
		}
		if (Input.GetKey(KeyCode.S))
		{
			if (running)
				control.SimpleMove(-cameraForwardDir*Time.deltaTime*runSpeed);
			else
				control.SimpleMove(-cameraForwardDir*Time.deltaTime*walkSpeed);
			if (verticalDir == MoveDir.Forward)
			{
				verticalDir = MoveDir.Idle;
			}
			else
			{
				verticalDir = MoveDir.Backward;
			}
		}
		
		
		if (Input.GetKey(KeyCode.A))
		{
			Vector3 left = RotateY(cameraForwardDir, -Mathf.PI/2);
			if (running)
				control.SimpleMove(left*Time.deltaTime*runSpeed);
			else
				control.SimpleMove(left*Time.deltaTime*walkSpeed);
			horizontalDir = MoveDir.Left;
		}
		if (Input.GetKey(KeyCode.D))
		{
			Vector3 right = RotateY(cameraForwardDir, Mathf.PI/2);
			if (running)
				control.SimpleMove(right*Time.deltaTime*runSpeed);
			else
				control.SimpleMove(right*Time.deltaTime*walkSpeed);
			if (horizontalDir == MoveDir.Left)
			{
				horizontalDir = MoveDir.Idle;
			}
			else
			{
				horizontalDir = MoveDir.Right;
			}
		}
		
		if (Input.GetKey(KeyCode.Z))
		{
			cameraForwardDir = RotateY(cameraForwardDir, Time.deltaTime);
			Vector3 forward = transform.position - cameraTransform.position;
			cameraTransform.forward = forward;
		}
		if (Input.GetKey(KeyCode.X))
		{
			cameraForwardDir = RotateY(cameraForwardDir, -Time.deltaTime);
			Vector3 forward = transform.position - cameraTransform.position;
			cameraTransform.forward = forward;
		}
		
		determineAnimation(horizontalDir, verticalDir);
		updateCameraPosition();
	}
	
	void rotateTowardMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray,out hit))
		{
			forwardDir = hit.point - transform.position;
			forwardDir.y = 0;
			forwardDir = forwardDir.normalized;
			transform.forward = forwardDir;
		}
	}
	
	void determineAnimation(MoveDir horizontal, MoveDir vertical)
	{
		Vector3 camera = cameraTransform.forward;
		camera.y = 0;
		
		if (vertical == MoveDir.Forward)
		{
			if (Vector3.Angle(transform.forward, camera) < 90)
			{
				anim.CrossFade(forwardClip.name);
			}
			else
			{
				anim.CrossFade(backwardClip.name);
			}
		}
		else if (vertical == MoveDir.Backward)
		{
			if (Vector3.Angle(transform.forward, camera) < 90)
			{
				anim.CrossFade(backwardClip.name);
			}
			else
			{
				anim.CrossFade(forwardClip.name);
			}
		}
		else
		{
			if (horizontal == MoveDir.Idle)
			{
				anim.CrossFade(idleClip.name);
			}
			else if (horizontal == MoveDir.Left)
			{
				if (Vector3.Angle(transform.forward, camera) < 90)
				{
					anim.CrossFade(leftClip.name);
				}
				else
				{
					anim.CrossFade(rightClip.name);
				}
			}
			else
			{
				if (Vector3.Angle(transform.forward, camera) < 90)
				{
					anim.CrossFade(rightClip.name);
				}
				else
				{
					anim.CrossFade(leftClip.name);
				}
			}
		}
	}
	
	void updateCameraPosition()
	{
		cameraTransform.position = transform.position - cameraDistance*cameraForwardDir;
		Vector3 pos = cameraTransform.position;
		pos.y = pos.y + cameraHeight;
		cameraTransform.position = pos;
	}
	
	Vector3 RotateY(Vector3 v, float angle )
    {
        float sin = Mathf.Sin( angle );
        float cos = Mathf.Cos( angle );       

        float tx = v.x;
        float tz = v.z;
        v.x = (cos * tx) + (sin * tz);
        v.z = (cos * tz) - (sin * tx);
		return v;
    }
}
