﻿using UnityEngine;
using System.Collections;
using System;

namespace Invector
{
    public class v_AIAnimator : v_AIMotor
    {
        #region AI Variables       
        // get Layers from the Animator Controller        
        public AnimatorStateInfo baseLayerInfo, rightArmInfo, leftArmInfo, fullBodyInfo, upperBodyInfo;

        int baseLayer { get { return animator.GetLayerIndex("Base Layer"); } }
        int rightArmLayer { get { return animator.GetLayerIndex("RightArm"); } }
        int leftArmLayer { get { return animator.GetLayerIndex("LeftArm"); } }
        int upperBodyLayer { get { return animator.GetLayerIndex("UpperBody"); } }
        int fullbodyLayer { get { return animator.GetLayerIndex("FullBody"); } }

        [HideInInspector]
        public Transform matchTarget;
        Vector3 lookPos;
        private float lookAtWeight;
        private bool resetState;

        #endregion

        /// <summary>
        /// Animation behaviour of the AI
        /// </summary>
        /// <param name="_speed"></param>
        /// <param name="_direction"></param>
        public void UpdateAnimator(float _speed, float _direction)
        {
            if (animator == null || !animator.enabled) return;

            LayerControl();

            LocomotionAnimation(_speed, _direction);
            QuickTurnAnimation(_direction);
            StepUpAnimation();
            JumpOverAnimation();
            ClimbUpAnimation();
            RollAnimation();
            CrouchAnimation();
            HitReactionAnimation();
            HitRecoilAnimation();
            MoveSetIDControl();
            MeleeATK_Animation();
            DEF_Animation();
            DeadAnimation();
        }

        void LayerControl()
        {
            baseLayerInfo = animator.GetCurrentAnimatorStateInfo(baseLayer);
            rightArmInfo = animator.GetCurrentAnimatorStateInfo(rightArmLayer);
            leftArmInfo = animator.GetCurrentAnimatorStateInfo(leftArmLayer);
            upperBodyInfo = animator.GetCurrentAnimatorStateInfo(upperBodyLayer);
            fullBodyInfo = animator.GetCurrentAnimatorStateInfo(fullbodyLayer);
        }

        /// <summary>
        /// Control the Locomotion behaviour of the AI
        /// </summary>
        /// <param name="_speed"></param>
        /// <param name="_direction"></param>
        void LocomotionAnimation(float _speed, float _direction)
        {
            onGround = (method != OffMeshLinkMoveMethod.JumpAcross && method != OffMeshLinkMoveMethod.DropDown);
            animator.SetBool("OnGround", onGround);
            var maxSpeed =(currentState.Equals(AIStates.PatrolSubPoints) || currentState.Equals(AIStates.PatrolWaypoints) ? patrolSpeed :
                          (OnStrafeArea ? strafeSpeed : chaseSpeed));

            _speed = Mathf.Clamp(_speed, -maxSpeed, maxSpeed);
            if (OnStrafeArea) _direction = Mathf.Clamp(_direction, -strafeSpeed, strafeSpeed);

            animator.SetFloat("Speed", actions ? 0 : (_speed != 0) ? _speed : 0, 0.25f, Time.fixedDeltaTime);
            animator.SetFloat("Direction", _direction, 0.1f, Time.fixedDeltaTime);
            animator.SetBool("Strafing", OnStrafeArea);
        }

        /// <summary>
        /// Trigger a Death by Animation, Animation with Ragdoll or just turn the Ragdoll On
        /// </summary>
        void DeadAnimation()
        {
            if (currentHealth > 0) return;

            CheckGroundDistance();

            transform.SendMessage("DropRightWeapon", SendMessageOptions.DontRequireReceiver);
            transform.SendMessage("DropLeftWeapon", SendMessageOptions.DontRequireReceiver);
            animator.SetTrigger("ResetState");
            _capsuleCollider.isTrigger = true;
            agent.enabled = false;

            if(groundDistance > 0.1f)
            {
                _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                _rigidbody.useGravity = true;
            }
            else            
            {
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                if (baseLayerInfo.IsName("Action.Dead") && baseLayerInfo.normalizedTime >= 1f)
                    RemoveComponents();
            }

	        if (deathBy == vCharacter.DeathBy.Animation)
            {
                animator.SetBool("Dead", true);                
            }
            else if (deathBy == vCharacter.DeathBy.AnimationWithRagdoll)
            {
                animator.SetBool("Dead", true);                
                if (baseLayerInfo.IsName("Action.Dead"))
                {
                    // activate the ragdoll after the animation finish played
                    if (baseLayerInfo.normalizedTime >= 0.8f)
                        SendMessage("ActivateRagdoll", SendMessageOptions.DontRequireReceiver);
                }
            }
            else if (deathBy == vCharacter.DeathBy.Ragdoll)
                SendMessage("ActivateRagdoll", SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// MOVE SET ID - check the Animator to see what MoveSet the character will move, also check your weapon to see if the moveset matches
        /// ps* Move Set is the way your character will move, ATK_ID is the way your character will attack. You can have different locomotion animations and attacks.
        /// </summary>
        void MoveSetIDControl()
        {
            if (meleeManager == null) return;

            animator.SetFloat("MoveSet_ID", meleeManager.CurrentMeleeAttack()!=null ? meleeManager.CurrentMeleeAttack().MoveSet_ID : 0);
        }

        /// <summary>
        /// Control Attack Behaviour
        /// </summary>
        void MeleeATK_Animation()
        {
            if (meleeManager == null) return;
            // ** IMPORTANT **
            // always reset the attack trigger on the last attack,
            // to make sure the character does not attack again when finish the last attack
            // if you created more attack states for different weapons like two handed, add the last clip here
            if (fullBodyInfo.IsName("BasicAttack.Attack C") || fullBodyInfo.IsName("Punch.Attack C"))
            {
                agent.enabled = false;
                animator.ResetTrigger("MeleeAttack");
            }

            if (actions) attackCount = 0;

            animator.SetBool("InAttack", meleeManager.applyDamage);
            animator.SetInteger("ATK_ID", meleeManager.CurrentMeleeAttack()!=null ? meleeManager.CurrentMeleeAttack().ATK_ID : 0);
        }

        /// <summary>
        /// ATTACK MELEE ANIMATION - it's activate by the AttackInput() method at the TPController by a trigger
        /// </summary>
        void DEF_Animation()
        {
            if (meleeManager == null) return;
            if (blocking)
                animator.SetInteger("DEF_ID", combatID.def);
            else
                animator.SetInteger("DEF_ID", 0);

            animator.SetBool("Mirror", combatID.mirror);
        }

        /// <summary>
        /// Trigger the Attack animation
        /// </summary>
        public void MeleeAttack()
        {
            if (animator != null && animator.enabled && !actions)
                animator.SetTrigger("MeleeAttack");
           
        }

        /// <summary>
        /// Trigger the HitReaction animation
        /// </summary>
        void HitReactionAnimation()
        {
            hitReaction = baseLayerInfo.IsTag("HitReaction");

            if (hitReaction)
            {
                if (meleeManager != null) meleeManager.applyDamage = false;
                animator.ResetTrigger("HitReaction");

                if (baseLayerInfo.normalizedTime > 0.1f)
                {
                    _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                    _rigidbody.useGravity = true;
                    agent.enabled = false;
                }

                CheckGroundDistance();

                if (!agent.enabled && !actions && !rolling && groundDistance < 0.3f)
                {
                    _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    _rigidbody.useGravity = false;
                    if (currentHealth > 0)
                        agent.enabled = true;
                }
            }
        }

        /// <summary>
        /// HIT RECOIL = trigger a recoil animation when you hit wall or shield, check the RecoilReaction script at the weapon
        /// </summary>
        void HitRecoilAnimation()
        {
            hitRecoil = baseLayerInfo.IsTag("HitRecoil");

            if (hitRecoil)
            {
                inAttack = false;
                if (meleeManager != null) meleeManager.applyDamage = false;
                animator.ResetTrigger("HitRecoil");
                canAttack = true;
                if (baseLayerInfo.normalizedTime > 0.1f)
                {
                    _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                    _rigidbody.useGravity = true;
                    agent.enabled = false;
                }
                CheckGroundDistance();
                if (!agent.enabled && !actions && !rolling && groundDistance < 0.3f)
                {
                    _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    _rigidbody.useGravity = false;
                    if (currentHealth > 0)
                        agent.enabled = true;
                }
            }
        }

        /// <summary>
        /// Trigger Recoil Animation - It's Called at the MeleeWeapon script using SendMessage
        /// </summary>
        public void TriggerRecoil(int recoil_id)
        {
            if (animator != null && animator.enabled)
            {
                animator.SetTrigger("ResetState");
                animator.SetInteger("Recoil_ID", recoil_id);
                animator.SetTrigger("HitRecoil");
            }                      
        }

        void QuickTurnAnimation(float _direction)
        {
            animator.SetBool("QuickTurn180", quickTurn);

            if (baseLayerInfo.IsName("Action.QuickTurn180"))
            {
                // exit state
                if (baseLayerInfo.normalizedTime > 0.9f)
                    quickTurn = false;
            }
        }

        void StepUpAnimation()
        {
            animator.SetBool("StepUp", stepUp);

            if (baseLayerInfo.IsName("Action.StepUp"))
            {
                // enter state
                if (baseLayerInfo.normalizedTime > 0.1f && baseLayerInfo.normalizedTime < 0.3f)
                {
                    agent.enabled = false;
                    quickTurn = false;
                    _capsuleCollider.isTrigger = true;
                    _rigidbody.useGravity = false;
                }

                // match target to find the target pos/rot                 
                if (!animator.IsInTransition(0))
                    animator.MatchTarget(matchTarget.position, matchTarget.rotation, AvatarTarget.LeftHand,
                                                          new MatchTargetWeightMask(new Vector3(0, 1, 1), 0f), 0f, 0.5f);

                // exit state
                if (baseLayerInfo.normalizedTime > 0.9f)               
					DisableAction (AIActions.StepUp);                
            }
        }

        void JumpOverAnimation()
        {
            animator.SetBool("JumpOver", jumpOver);

            if (baseLayerInfo.IsName("Action.JumpOver"))
            {
                // enter state
                if (baseLayerInfo.normalizedTime > 0.01f && baseLayerInfo.normalizedTime < 0.3f)
                {
                    agent.enabled = false;
                    quickTurn = false;
                    _capsuleCollider.isTrigger = true;
                    _rigidbody.useGravity = false;
                }

                // match target to find the target pos/rot 
                if (!animator.IsInTransition(0))
                    animator.MatchTarget(matchTarget.position, matchTarget.rotation,
                                AvatarTarget.LeftHand, new MatchTargetWeightMask
                                (new Vector3(0, 1, 1), 0), 0.1f * (1 - baseLayerInfo.normalizedTime), 0.3f * (1 - baseLayerInfo.normalizedTime));

                // exit state
                if (baseLayerInfo.normalizedTime >= 0.7f)                
					DisableAction (AIActions.JumpOver);                
            }
        }

        void ClimbUpAnimation()
        {
            animator.SetBool("ClimbUp", climbUp);

            if (baseLayerInfo.IsName("Action.ClimbUp"))
            {
                // enter state
                if (baseLayerInfo.normalizedTime > 0.01f && baseLayerInfo.normalizedTime < 0.3f)
                {
                    agent.enabled = false;
                    quickTurn = false;
                    _rigidbody.useGravity = false;
                    _capsuleCollider.isTrigger = true;
                }

                // match target to find the target pos/rot 
                if (!animator.IsInTransition(0))
                    animator.MatchTarget(matchTarget.position, matchTarget.rotation,
                               AvatarTarget.LeftHand, new MatchTargetWeightMask
                               (new Vector3(0, 1, 1), 0), 0f, 0.2f);
                // exit state
                if (crouch)
                {
                    if (baseLayerInfo.normalizedTime >= 0.7f)                    
						DisableAction (AIActions.ClimbUp);
                }
                else
                {
                    if (baseLayerInfo.normalizedTime >= 0.9f)                    
						DisableAction (AIActions.ClimbUp);                   
                }
            }
        }

		void CrouchAnimation()
        {
            animator.SetBool("Crouch", crouch);

            if (animator != null && animator.enabled)
                CheckAutoCrouch();
        }

        protected void RollAnimation()
        {
            if (animator == null || animator.enabled == false) return;

            animator.SetBool("Roll", rolling);
            if (baseLayerInfo.IsName("Action.Roll"))
            {
                CheckGroundDistance();
                // reset the rigidbody a little ealier to fall while on air              
                if (baseLayerInfo.normalizedTime > 0.3f)
                {
                    _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                    _rigidbody.useGravity = true;
                    agent.enabled = false;
                }

                // exit state
                if (!crouch && baseLayerInfo.normalizedTime > 0.85f)
                {
                    rolling = false;
                    ResetAIRotation();
                }
                else if (crouch && baseLayerInfo.normalizedTime > 0.75f)
                {
                    rolling = false;
                    ResetAIRotation();
                }
            }          

            if (!agent.enabled && !actions && !rolling && groundDistance < 0.3f)
            {
                _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                _rigidbody.useGravity = false;
                if (currentHealth > 0)
                    agent.enabled = true;
            }
        }

        void ResetAIRotation()
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        protected void CheckForwardAction()
        {
            var hitObject = CheckActionObject();
            if (hitObject != null)
            {
                try
                {
                    if (hitObject.CompareTag("ClimbUp"))
						DoAction(hitObject, ref climbUp, AIActions.ClimbUp);
                    else if (hitObject.CompareTag("StepUp"))
						DoAction(hitObject, ref stepUp, AIActions.StepUp);
                    else if (hitObject.CompareTag("JumpOver"))
						DoAction(hitObject, ref jumpOver, AIActions.JumpOver);
                }
                catch (UnityException e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
            else
            {
                if (agent.enabled)
                {
                    agent.ResetPath();
                    method = OffMeshLinkMoveMethod.Grounded;
                }
            }
        }

		/// <summary>
		/// Does a specific action.
		/// </summary>
		/// <param name="hitObject">Hit object.</param>
		/// <param name="action">Action.</param>
		/// <param name="aiAction">Ai action.</param>
		void DoAction(GameObject hitObject, ref bool action, AIActions aiAction)
        {
            var triggerAction = hitObject.transform.GetComponent<vTriggerAction>();
            if (!triggerAction)
            {
                Debug.LogWarning("Missing TriggerAction Component on " + hitObject.transform.name + "Object");
                return;
            }

            if (!actions)
            {
                matchTarget = triggerAction.target;
                var rot = hitObject.transform.rotation;
                transform.rotation = rot;
                animator.SetTrigger("ResetState");
                action = true;
				SetActiveAction (aiAction, false);
            }
        }

		/// <summary>
		/// Disables the specific action.
		/// </summary>
		/// <param name="aiAction">Ai action.</param>
		void DisableAction(AIActions aiAction)
		{	
			switch (aiAction) 
			{
			case AIActions.ClimbUp:
				if(climbUp ==true)
					StartCoroutine(SetActiveAction(aiAction,true,8f));
				climbUp = false;
				break;
			case AIActions.JumpOver:
				if(jumpOver ==true)
					StartCoroutine(SetActiveAction(aiAction,true,5f));
				jumpOver = false;
				break;
			case AIActions.StepUp:
				if(stepUp ==true)
					StartCoroutine(SetActiveAction(aiAction,true,5f));
				stepUp = false;
				break;
			}
			_capsuleCollider.isTrigger = false;
			_rigidbody.useGravity = true;
			agent.enabled = true;
			quickTurn = false;
		}

        void OnAnimatorMove()
        {
            if (agent.enabled)
                agent.velocity = animator.deltaPosition / Time.deltaTime;

            if (!_rigidbody.useGravity && !actions)
                _rigidbody.velocity = animator.deltaPosition;

            // Strafe Movement
            if (OnStrafeArea && !actions && target != null && canSeeTarget())
            {
                Vector3 targetDir = target.position - transform.position;
                float step = (meleeManager != null && meleeManager.applyDamage) ? attackRotationSpeed * Time.deltaTime : (strafeRotationSpeed * Time.deltaTime);
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
                var rot = Quaternion.LookRotation(newDir);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, rot.eulerAngles.y, transform.eulerAngles.z);
            }
            // Rotate the Character to the OffMeshLink End
            else if (agent.isOnOffMeshLink && !actions)
            {
                var pos = agent.nextOffMeshLinkData.endPos;
                targetPos = pos;
                OffMeshLinkData data = agent.currentOffMeshLinkData;
                desiredRotation = Quaternion.LookRotation(new Vector3(data.endPos.x, transform.position.y, data.endPos.z) - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, (agent.angularSpeed * 2f) * Time.deltaTime);
            }
            // Free Movement
            else if (agent.desiredVelocity.magnitude > 0.1f && !actions && agent.enabled)
            {
                if (meleeManager != null && meleeManager.applyDamage)
                {
                    desiredRotation = Quaternion.LookRotation(agent.desiredVelocity);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, agent.angularSpeed * attackRotationSpeed * Time.deltaTime);
                }
                else
                {
                    desiredRotation = Quaternion.LookRotation(agent.desiredVelocity);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, agent.angularSpeed * Time.deltaTime);
                }
            }
            // Use the Animator rotation while doing an Action
            else if (actions || currentHealth <= 0f || inAttack)
            {
                if (rolling)
                {
                    desiredRotation = Quaternion.LookRotation(rollDirection - transform.position);
                    transform.rotation = desiredRotation;
                }
                else
                    transform.rotation = animator.rootRotation;

                // Use the Animator position while doing an Action
                if (!agent.enabled) transform.position = animator.rootPosition;
            }
        }

        /// <summary>
        /// Head Track IK
        /// </summary>
        void OnAnimatorIK()
        {
            if (headWeight == 0 || target == null) return;
            
            if (currentState == AIStates.Chase && onFovAngle() && !actions && target != null && baseLayerInfo.IsTag("HeadTrack"))
                lookAtWeight += headTrackMultiplier * Time.deltaTime;
            else
                lookAtWeight -= headTrackMultiplier * Time.deltaTime;

            lookAtWeight = Mathf.Clamp(lookAtWeight, 0f, 1f);
            if (OnStrafeArea)
                animator.SetLookAtWeight(lookAtWeight, strafeBodyWeight, headWeight);
            else
                animator.SetLookAtWeight(lookAtWeight, freeBodyWeight, headWeight);
            if (headTarget != null)
            {
                lookPos = Vector3.Lerp(lookPos, headTarget.position, 2f * Time.deltaTime);
                animator.SetLookAtPosition(lookPos);
            }                
        }
    }
}
