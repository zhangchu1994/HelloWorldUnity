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
//		bool m_AttackMove = false;
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

		public void SetDestinationParent(Vector3 point)
		{
			agent.stoppingDistance = 1f;
			agent.destination = point;
			m_MainActor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);
			m_MainActor.m_ActorObject.transform.LookAt (point);
			agent.updateRotation = true;
		}


		public void SetDestination(Vector3 point,Vector3 normal)
		{
//			m_MainActor.SetActorStatus (Actor.ActorStatus.Agent);
			RemoveAgentFlag();

			if (m_MainActor.IsActorStatus(Actor.ActorStatus.AgentToAttack) == true)
				agent.stoppingDistance = 3.5f;
			else
				agent.stoppingDistance = 0;

			InitAgentFlag (point,normal);
			agent.destination = point;
			m_MainActor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);
			m_MainActor.m_ActorObject.transform.LookAt (point);
			agent.updateRotation = true;
		}


		void InitAgentFlag(Vector3 point,Vector3 normal)
		{
			Quaternion q = new Quaternion();
			if (normal != null)
				q.SetLookRotation(normal, Vector3.forward);
			particleClone = Instantiate(particle, point, q);
		}

		void RemoveAgentFlag()
		{
			if (particleClone != null)
			{
				GameObject.Destroy(particleClone);
				particleClone = null;
			}
		}


		protected bool IsAgentDone()
		{
			return !agent.pathPending && AgentStopping();
		}

		protected bool AgentStopping()
		{
			return agent.remainingDistance <= agent.stoppingDistance;
		}

		void Update () 
		{
			if (m_MainActor.m_ActorStatus == Actor.ActorStatus.Agent || m_MainActor.m_ActorStatus == Actor.ActorStatus.AgentToAttack) 
			{
				if (IsAgentDone())
				{
					RemoveAgentFlag ();
					m_MainActor.AgentDone ();
				}
				else
				{

				}
			}
		}


	}
}
