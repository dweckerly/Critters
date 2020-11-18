using UnityEngine;

[RequireComponent(typeof(Critter))]
[RequireComponent(typeof(CharacterController))]
public class CritterController : MonoBehaviour
{
	Critter critter;
    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;

	float changeDirectionTime = 0f;
	float minDirectionChangeInterval = 0.5f;
	float maxDirectionChangeInterval = 3f;
	float maxHeadingChange = 90f;

	bool idle = false;
	float idleChance = 0.3f;

	float idleWanderSwitchTime = 0f;
	float minIWSTime = 2f;
	float maxIWSTime = 4f;

	float heading = 0f;
	Vector3 targetRotation = Vector3.zero;

	float gravity = 10f;

	public bool flying = false;

    private void Start()
    {
		critter = GetComponent<Critter>();
		controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if (hit.gameObject.tag == "Perimeter")
		{
			
		}
	}

    void CritterMove()
	{
		if(!flying)
        {
			moveDirection.y -= gravity;
		}
		
		if (!idle)
        {
			if(critter == null)
            {
				Debug.Log("Critter is null...");
				critter = GetComponent<Critter>();
			}
			if (!critter.animator.GetBool("moving"))
            {
				critter.animator.SetBool("moving", true);
			}
			moveDirection = moveDirection.normalized * critter.speed;
			if (controller == null)
			{
				Debug.Log("Controller is null...");
				controller = GetComponent<CharacterController>();
			}
			controller.Move(moveDirection * Time.deltaTime);
		}
	}

	void CritterFlyMove()
    {
		if (!critter.animator.GetBool("moving"))
		{
			critter.animator.SetBool("moving", true);
		}
		moveDirection = moveDirection.normalized * critter.speed;
		controller.Move(moveDirection * Time.deltaTime);
	}

	public void MoveAwayFromTarget()
	{
		if(idle)
        {
			idle = false;
        }
		if (critter.target != null)
		{
			moveDirection = transform.position - critter.target.position;
		}
		CritterMove();
	}

	public void MoveTowardsTarget()
	{
		if (idle)
		{
			idle = false;
		}
		if(critter.target != null)
        {
			if(critter.IsWithinInteractDistance())
            {
				Idle();
            }
			else
            {
				moveDirection = critter.target.position - transform.position;
			}
			
		}
		CritterMove();
	}

	public void MoveTowardsSpecificTarget(Transform _target)
    {
		if (idle)
		{
			idle = false;
		}
		if (_target != null)
		{
			if (critter.IsWithinInteractDistance())
			{
				Idle();
			}
			else
			{
				moveDirection = _target.position - transform.position;
			}

		}
		CritterMove();
	}

	public void Idle()
	{
		if (critter.animator.GetBool("moving"))
		{
			critter.animator.SetBool("moving", false);
		}
		if(!idle)
        {
			idle = true;
        }
		CritterMove();
	}

	public void WanderAndIdle()
    {
		CheckIdleOrWanderSwitch();
		if (idle)
		{
			Idle();
		}
		else
		{
			Wander();
		}
	}

	void WanderDirectionCalc()
    {
		if (idle)
		{
			idle = false;
		}
		CheckDirectionInterval();
		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime);
		moveDirection = transform.TransformDirection(Vector3.forward);
	}

	public void Wander()
	{
		WanderDirectionCalc();
		CritterMove();
	}

	public void FlyWander()
    {
		WanderDirectionCalc();
		CritterFlyMove();
    }

	void CheckDirectionInterval()
    {
		if(Time.time >= changeDirectionTime)
        {
			changeDirectionTime = SetInterval(minDirectionChangeInterval, maxDirectionChangeInterval);
			ChangeDirection();
        } 
    }

	void ChangeDirection()
	{
		heading = Random.Range(transform.eulerAngles.y - maxHeadingChange, transform.eulerAngles.y + maxHeadingChange);
		targetRotation = new Vector3(0, heading, 0);
	}

	void CheckIdleOrWanderSwitch()
    {
		if(Time.time > idleWanderSwitchTime)
        {
			idleWanderSwitchTime = SetInterval(minIWSTime, maxIWSTime);
			idle = WanderOrIdle();
        }
    }

	bool WanderOrIdle()
    {
		float rand = Random.Range(0f, 1f);
		if(rand < idleChance)
        {
			return true;
        }
		return false;
    }

	float SetInterval(float minInterval, float maxInterval)
	{
		return Time.time + Random.Range(minInterval, maxInterval);
	}
}
