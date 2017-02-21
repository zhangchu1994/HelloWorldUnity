using UnityEngine;
using System.Collections;
using GlobalGame;

namespace GlobalGame 
{
	public class ActorAgentManager : MonoBehaviour 
	{

		public Object particle;
		protected UnityEngine.AI.NavMeshAgent agent;
	//	protected Animator animator;
	//	protected Locomotion locomotion;
		public Actor m_MainActor;
		bool m_AttackMove = false;
		protected Object particleClone;

		public ActorAgentManager()
		{
			
		}


		// Use this for initialization
		void Start () 
		{
			m_MainActor = GetComponent<Actor> ();
			agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
			agent.updateRotation = false;

			particle = Resources.Load ("Effect/CircleFX_Dark");
			particleClone = null;

//		animator = GetComponent<Animator>();
//		locomotion = new Locomotion(animator);


//			m_agent = (NavMeshAgent)m_ActorObject.GetComponent("NavMeshAgent");
//			m_agent.speed = 15;
//			m_agent.stoppingDistance = 0.1f; 
//			m_agent.radius = 0.5f;
//			m_agent.acceleration = 15;
//			m_agent.autoRepath = true;
//			m_agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
		}

//		public void SetDestinationWithClick()
//		{
//			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit = new RaycastHit();
//			if (Physics.Raycast(ray, out hit))
//			{
//				SetDestination (hit.point, hit.normal,false);
//			}
//		}

		public void SetDestination(Vector3 point,Vector3 normal,bool argAttack)
		{
			if (particleClone != null)
			{
				GameObject.Destroy(particleClone);
				particleClone = null;
			}

			m_AttackMove = argAttack;
			// Create a particle if hit

			if (argAttack == true)
				agent.stoppingDistance = 5;
			else
				agent.stoppingDistance = 0;
			Quaternion q = new Quaternion();
			if (normal != null)
				q.SetLookRotation(normal, Vector3.forward);
			particleClone = Instantiate(particle, point, q);

			agent.destination = point;
			m_MainActor.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);
			m_MainActor.m_ActorObject.transform.LookAt (point);
			agent.updateRotation = true;
		}

		protected void SetupAgentLocomotion()
		{
			if (AgentDone())
			{
	//			locomotion.Do(0, 0);
				if (particleClone != null)
				{
					GameObject.Destroy(particleClone);
					particleClone = null;
				}
				if (m_AttackMove == true)
					m_MainActor.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop);
				else						
					m_MainActor.PlayAnimation (Global.BattleAnimationType.Stand, WrapMode.Loop);
			}
			else
			{
	//			float speed = agent.desiredVelocity.magnitude;
	//			Vector3 velocity = Quaternion.Inverse(transform.rotation) * agent.desiredVelocity;
	//			float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;
	//			locomotion.Do(speed, angle);
			}
		}

	//    void OnAnimatorMove()
	//    {
	//        agent.velocity = animator.deltaPosition / Time.deltaTime;
	//		transform.rotation = animator.rootRotation;
	//    }

		protected bool AgentDone()
		{
			return !agent.pathPending && AgentStopping();
		}

		protected bool AgentStopping()
		{
			return agent.remainingDistance <= agent.stoppingDistance;
		}

		// Update is called once per frame
		void Update () 
		{
//			if (Input.GetButtonDown ("Fire1")) 
//				SetDestinationWithClick();
			
			SetupAgentLocomotion();
		}
	}
}
