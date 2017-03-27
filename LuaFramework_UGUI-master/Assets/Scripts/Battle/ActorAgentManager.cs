using UnityEngine;
using System.Collections;
using GlobalGame;
using UnityEngine.AI;

namespace GlobalGame 
{
	public class ActorAgentManager : MonoBehaviour 
	{

		public Object particle;
		public NavMeshAgent agent;
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
			agent.ResetPath();
			agent.Resume ();
			agent.stoppingDistance = 1f;
			agent.destination = point;
			m_MainActor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);
			m_MainActor.m_ActorObject.transform.LookAt (point);
			agent.updateRotation = true;
		}


		public void SetDestination(Vector3 point,Vector3 normal,float stopDis)
		{
//			m_MainActor.SetActorStatus (Actor.ActorStatus.Agent);
			agent.ResetPath();
			agent.Resume ();
//			Global.BattleLog(m_MainActor,"SetDestination");
			RemoveAgentFlag();

			if (m_MainActor.IsActorStatus (Actor.ActorStatus.AgentToAttack) == true) 
			{
//				Debug.Log(this.gameObject.name + " Width = "+stopDis);
				agent.stoppingDistance = stopDis;//3.5f;
			}
			else
				agent.stoppingDistance = 0;

//			InitAgentFlag (point,normal);
			agent.destination = point;
			m_MainActor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);
			m_MainActor.m_ActorObject.transform.LookAt (point);
			agent.updateRotation = true;
		}

		#region Flag
		void InitAgentFlag(Vector3 point,Vector3 normal)
		{
			Quaternion q = new Quaternion();
//			if (normal != null)
//				q.SetLookRotation(normal, Vector3.forward);
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
		#endregion

		protected bool IsAgentDone()
		{
			return !agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance);
		}

//		protected bool AgentStopping()
//		{
//			return ;
//		}

		public void StopAgent()
		{
//			Global.BattleLog (m_MainActor,"StopAgent");
			agent.Stop ();
		}

		void Update () 
		{
//			if (m_MainActor.IsActorStatus (Actor.ActorStatus.Attack)) 
//			{
//				Debug.Log ("(m_MainActor.IsActorStatus (Actor.ActorStatus.Attack)__________________");
//				agent.Stop ();
//				return;
//			}
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
