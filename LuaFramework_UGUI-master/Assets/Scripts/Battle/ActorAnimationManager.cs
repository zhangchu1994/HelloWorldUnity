using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class ActorAnimationManager : MonoBehaviour 
	{
		public Animation animationController = null;
		public Actor m_MainActor;

		// Use this for initialization
		void Start () 
		{
			
		}

		public void InitAnimation()
		{
			m_MainActor = GetComponent<Actor> ();
			animationController = m_MainActor.GetComponent<Animation>();
			PlayAnimation (Global.BattleAnimationType.Stand,WrapMode.Loop);
		}

		// Update is called once per frame
		void Update () 
		{
			UpdateAnimation ();
		}

		public void PlayAnimation(Global.BattleAnimationType argType,WrapMode mode,bool isStop=false)
		{
			if (animationController.IsPlaying (Global.GetAnimation (argType)) == true)
				return;
//			if (m_MainActor.name == "Actor2")
//				Debug.Log ("Name = "+m_MainActor.name+" Type = "+argType);
			animationController.Stop ();
			animationController.wrapMode = mode;
			animationController.Play(Global.GetAnimation(argType));
			if (argType == Global.BattleAnimationType.Dead)
				StartCoroutine (WaitThenDoThings(animationController[Global.Die].length));
		}

		IEnumerator WaitThenDoThings(float time)
		{
			yield return new WaitForSeconds(time);
			m_MainActor.ActorDeadAnimationDone ();
			m_MainActor.m_ActorUIManager.ActorDead ();
			// Now do some stuff...
//			animation.CrossFade("anotherAnim", 0.5f);
//			Debug.Log("Next Animation____"+time);
		}

		public void PlayAnimations(List<Global.BattleAnimationType> argTypes,WrapMode mode,bool isStop=false)
		{
//			if (animationController.IsPlaying (Global.GetAnimation (argType)) == true)
//				return;
			//			if (m_MainActor.name == "Actor2")
			//				Debug.Log ("Name = "+m_MainActor.name+" Type = "+argType);
			animationController.Stop ();
			animationController.wrapMode = mode;

			for (int i = 0; i < argTypes.Count; i++) 
			{
				Global.BattleAnimationType type = argTypes [i];
				animationController.PlayQueued(Global.GetAnimation(type));
			}
		}
		//		public void PlayStand () {
		//			animationController.wrapMode = WrapMode.Loop;
		//			animationController.Play((Global.GetAnimation(Global.BattleAnimationType.Stand)));
		//			animationState = 0;
		//		}

		//		public void PlayAttack () 
		//		{
		//			animationController.wrapMode = WrapMode.Once;
		//			animationController.PlayQueued("attack1");
		//			animationController.PlayQueued("attack2");
		//			animationController.PlayQueued("attack3");
		//			animationController.PlayQueued("attack4");
		//			animationState = 1;
		//		}


		void UpdateAnimation()
		{
//			animationController.
//			if (animationController.AddClip)
//			{
//				// animation finished...
//				Debug.Log("UpdateAnimation____________________"+AnimationState.name);
//			}
		}


	}
}
