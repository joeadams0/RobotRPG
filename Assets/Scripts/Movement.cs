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
	
	int currentGun;
	int currentAbility;
	
	Animation anim;
	
	ParticleSystem bullets;
	
	UnitData unitData;
	AttackScript attackScript;
	
	bool running;
	
	Player player;
	
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
		
		player = GetComponent<Player>();
		
		bullets = (ParticleSystem)GameObject.FindGameObjectWithTag("gun").GetComponent<ParticleSystem>();
		
		unitData = GetComponent<UnitData>();
		attackScript = GetComponent<AttackScript>();
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
		
		switchGun();
		switchAbility();
		fireGun();
	}
	
	void switchGun()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			currentGun = (currentGun + player.numGuns() - 1) % player.numGuns ();
			player.switchGun(currentGun);
		}
		else if(Input.GetKey (KeyCode.E))
		{
			currentGun = (currentGun + 1) % player.numGuns();
			player.switchGun(currentGun);
		}
	}
	
	void switchAbility()
	{
		int ability = -1;
		if (Input.GetKey (KeyCode.Alpha1))
		{
			ability = 1;
		}
		else if(Input.GetKey (KeyCode.Alpha2))
		{
			ability = 2;
		}
		else if(Input.GetKey (KeyCode.Alpha3))
		{
			ability = 3;
		}
		else if(Input.GetKey (KeyCode.Alpha4))
		{
			ability = 4;
		}
		else if(Input.GetKey (KeyCode.Alpha5))
		{
			ability = 5;
		}
		if (player.numAbilities() > ability - 1 && ability != -1)
		{
			player.switchAbility(ability-1);
			currentAbility = ability - 1;
		}
	}
	
	void fireGun()
	{
		if (Input.GetMouseButton(0))
		{
			if (player.fireGun())
			{
				bullets.Play();
				attackScript.attack(forwardDir, player.gunRange(), AttackScript.ATTACK_TYPE_BULLET, player.gunDamage(), unitData.UnitType);
			}
		}
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
