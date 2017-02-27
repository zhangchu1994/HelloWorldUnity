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
			animationController.Stop ();
			animationController.wrapMode = mode;
			animationController.Play(Global.GetAnimation(argType));
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

		}

	}
}
