﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using com.ootii.Base;
using com.ootii.Cameras;
using com.ootii.Helpers;
using com.ootii.Input;
using com.ootii.Utilities.Debug;

namespace com.ootii.AI.Controllers
{
    /// <summary>
    /// The exploration run allows the avatar to move walk, jog, and run
    /// forward, but to also rotate towards the camera. This is a modern
    /// 3rd person 'action/adventure' motion where the avatar stays in view
    /// of the camera, but can run forward, to the side, or towards the camera.
    /// 
    /// When running to the side, the avatar typically rotates around the camera.
    /// </summary>
    [MotionTooltip("A forward walk/run blend that allows the avatar to rotate towards the camera. Best when used with the Adventure Camera.")]
    public class AdventureForward : MotionControllerMotion
    {
        // Enum values for the motion
        public const int PHASE_UNKNOWN = 0;
        public const int PHASE_START = 400;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AdventureForward()
            : base()
        {
            _Priority = 1;
            mIsStartable = true;
            mIsGroundedExpected = true;
        }

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="rController">Controller the motion belongs to</param>
        public AdventureForward(MotionController rController)
            : base(rController)
        {
            _Priority = 1;
            mIsStartable = true;
            mIsGroundedExpected = true;
        }

        /// <summary>
        /// Preprocess any animator data so the motion can use it later
        /// </summary>
        public override void LoadAnimatorData()
        {
#if UNITY_4_0 || UNITY_4_0_1 ||UNITY_4_1|| UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
            string lBaseLayer = "";
#else
            string lBaseLayer = "Base Layer.";
#endif

            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.Forward");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft90");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft135");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft180");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToRight90");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToRight135");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToRight180");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft90");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft135");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight90");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight135");
            mController.AddAnimatorName("Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotate180");

            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.Forward");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft90");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft135");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft180");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToRight90");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToRight135");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToRight180");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft90");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft135");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight90");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight135");
            mController.AddAnimatorName("AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotate180");

            mController.AddAnimatorName(lBaseLayer + "Idle-SM.Idle_Casual -> " + lBaseLayer + "AdventureForward-SM.Forward");

            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleToLeft90");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleToLeft135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleToLeft180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleToRight90");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleToRight135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleToRight180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleRotateLeft90");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleRotateLeft135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleRotateRight90");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleRotateRight135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.IdleRotate180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Forward");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Run");

            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "Idle-SM.Idle_Casual");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Forward -> " + lBaseLayer + "Idle-SM.Idle_Casual");

            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunLeft135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunLeft135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunLeft135 -> " + lBaseLayer + "AdventureForward-SM.Run");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunLeft180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunLeft180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunLeft180 -> " + lBaseLayer + "AdventureForward-SM.Run");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunRight135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunRight135");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunRight135 -> " + lBaseLayer + "AdventureForward-SM.Run");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunRight180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunRight180");
            mController.AddAnimatorName(lBaseLayer + "AdventureForward-SM.RunRight180 -> " + lBaseLayer + "AdventureForward-SM.Run");
        }

        /// <summary>
        /// Tests if this motion should be started. However, the motion
        /// isn't actually started.
        /// </summary>
        /// <returns></returns>
        public override bool TestActivate()
        {
            // We let the ExplorationRun take over if we're in 
            // the traversal stance and groundes. There must be an 
            // attempt to move the avatar with some input/AI.
            if (!mIsStartable) { return false; }
            if (!mController.IsGrounded) { return false; }

            ControllerState lState = mController.State;
            if (lState.InputMagnitudeTrend.Value < 0.1f) { return false; }
            
            if (lState.Stance != EnumControllerStance.TRAVERSAL) { return false; }
                
            return true;
        }

        /// <summary>
        /// Tests if the motion should continue. If it shouldn't, the motion
        /// is typically disabled
        /// </summary>
        /// <returns></returns>
        public override bool TestUpdate()
        {
            if (mIsActivatedFrame) { return true; }

            if (!IsInRunState && mController.GetAnimatorMotionPhase(mMotionLayer.AnimatorLayerIndex) != AdventureForward.PHASE_START) { return false; }

            if (!mController.IsGrounded) { return false; }

            ControllerState lState = mController.State;
            if (lState.InputMagnitudeTrend.Average == 0f) { return false; }
            
            if (lState.Stance != EnumControllerStance.TRAVERSAL) { return false; }

            return true;
        }

        /// <summary>
        /// Called to start the specific motion. If the motion
        /// were something like 'jump', this would start the jumping process
        /// </summary>
        /// <param name="rPrevMotion">Motion that this motion is taking over from</param>
        public override bool Activate(MotionControllerMotion rPrevMotion)
        {
            // Store the last camera mode and force it to a fixed view.
            // We do this to always keep the camera behind the player
            if (mController.UseInput && mController.CameraRig != null)
            {
                mController.CameraRig.TransitionToMode(EnumCameraMode.THIRD_PERSON_FOLLOW);
            }

            // It's possible we're activating from a small fall or other
            // skip while already in this motion. If so, no need to restart it.
            if (!IsInRunState)
            {
                // Trigger the change in the animator
                mController.SetAnimatorMotionPhase(mMotionLayer.AnimatorLayerIndex, AdventureForward.PHASE_START, true);
            }

            return base.Activate(rPrevMotion);
        }

        /// <summary>
        /// Called to stop the motion. If the motion is stopable. Some motions
        /// like jump cannot be stopped early
        /// </summary>
        public override void Deactivate()
        {
            base.Deactivate();
        }

        /// <summary>
        /// Updates the motion over time. This is called by the controller
        /// every update cycle so animations and stages can be updated.
        /// </summary>
        public override void UpdateMotion()
        {
            if (!TestUpdate())
            {
                Deactivate();
                return;
            }

            // If we're blocked, we're going to modify the speed
            // in order to blend into and out of a stop
            if (mController.State.IsForwardPathBlocked)
            {
                float lAngle = Vector3.Angle(mController.State.ForwardPathBlockNormal, mController.transform.forward);

                float lDiff = 180f - lAngle;
                float lSpeed = mController.State.InputMagnitudeTrend.Value * (lDiff / mController.ForwardBumperBlendAngle);

                mController.State.InputMagnitudeTrend.Replace(lSpeed);
            }

            // If we're running, control the speed and direction
            if (IsInRunState)
            {
                DetermineAngularVelocity();
                DetermineVelocity();
            }

#if UNITY_4_0 || UNITY_4_0_1 ||UNITY_4_1|| UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
            string lBaseLayer = "";
#else
            string lBaseLayer = "Base Layer.";
#endif

            // Trend data allows us to wait for the speed to peak or bottom-out before we send it to
            // the animator. This is important for pivots that need to be very precise.
            string lStateName = mController.GetAnimatorStateName(mMotionLayer.AnimatorLayerIndex);

            if (lStateName == lBaseLayer + "AdventureForward-SM.Run" ||
                lStateName == lBaseLayer + "AdventureForward-SM.RunLeft135" ||
                lStateName == lBaseLayer + "AdventureForward-SM.RunLeft180" ||
                lStateName == lBaseLayer + "AdventureForward-SM.RunRight135" ||
                lStateName == lBaseLayer + "AdventureForward-SM.RunRight180"
                )
            {
                mUseTrendData = true;
            }
            else
            {
                mUseTrendData = false;
            }
        }

        /// <summary>
        /// Returns the current angular velocity of the motion
        /// </summary>
        protected override Vector3 DetermineAngularVelocity()
        {
            mAngularVelocity = Vector3.zero;

            // Ensure we're not in a pivot. The pivot will do the rotating.
            if (!IsInPivotState)
            {
                float lAngularSpeed = mController.State.InputFromAvatarAngle * mController.RotationSpeed;
                
                // If we're reaching a pivot point, slow the speed down so we have 
                // time to perform the pivot
                if (Mathf.Abs(mController.State.InputFromAvatarAngle) > 10)
                {
                    lAngularSpeed /= 90f;
                }
                // If our angular speed exceeds the angle, limit it to the angle
                else if (Mathf.Abs(lAngularSpeed * Time.deltaTime) > Mathf.Abs(mController.State.InputFromAvatarAngle)) 
                { 
                    lAngularSpeed = mController.State.InputFromAvatarAngle / Time.deltaTime;
                }

                mAngularVelocity.y = lAngularSpeed;
            }

            return mAngularVelocity;
        }

        /// <summary>
        /// Allows the motion to modify the velocity before it is applied.
        /// </summary>
        /// <returns></returns>
        public override void CleanRootMotion(ref Vector3 rVelocityDelta, ref Quaternion rRotationDelta)
        {
            // Remove any x movement. This will prevent swaying
            rVelocityDelta.x = 0f;

            // In this motion, there is mo moving backwards
            if (rVelocityDelta.z < 0f)
            {
                rVelocityDelta.z = 0f;
            }

            // Don't allow rotation while we're moving forward. However, we
            // need to allow it with pivots.
            if (!IsInPivotState)
            {
                //string lState = mController.GetAnimatorStateTransitionName(mAnimatorLayerIndex);

                //if (lState == "Idle-SM.Idle_Casual -> AdventureForward-SM.Forward" ||
                //    lState == "AnyState -> AdventureForward-SM.Forward" ||
                //    lState == "AdventureForward-SM.Forward" ||
                //    (lState == "AdventureForward-SM.Run")
                //   )
                //{
                    rRotationDelta = Quaternion.identity;
                //}
            }
        }

        /// <summary>
        /// Raised when the animator's state has changed
        /// </summary>
        /// <param name="rLastStateID">State the animator is leaving</param>
        /// <param name="rNewStateID">State the animator is now at</param>
        public override void OnAnimatorStateChange(int rLastStateID, int rNewStateID)
        {
            //// If we're active (and get here), we need to prevent the AnyState
            //if (mController.GetAnimatorMotionPhase(mMotionLayer.AnimatorLayerIndex) == AdventureForward.PHASE_START)
            //{
            //    mController.SetAnimatorMotionPhase(mMotionLayer.AnimatorLayerIndex, AdventureForward.PHASE_UNKNOWN);
            //}
        }

        /// <summary>
        /// Test to see if we're currently in the locomotion state
        /// </summary>
        public bool IsInRunState
        {
            get
            {
                string lState = mController.GetAnimatorStateName(mMotionLayer.AnimatorLayerIndex);
                string lTransition = mController.GetAnimatorStateTransitionName(mMotionLayer.AnimatorLayerIndex);

                // Do a simple test for the substate name
                if (lState.Length == 0) { return false; }
                if (lState.IndexOf("AdventureForward-SM") >= 0 || lTransition.IndexOf("AdventureForward-SM") >= 0)
                {
                    return true;
                }

                return false;
            }
        }
        
        /// <summary>
        /// Test to see if we're currently pivoting
        /// </summary>
        public bool IsInPivotState
        {
            get
            {
#if UNITY_4_0 || UNITY_4_0_1 ||UNITY_4_1|| UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
                    string lBaseLayer = "";
#else
                string lBaseLayer = "Base Layer.";
#endif

                string lState = mController.GetAnimatorStateName(mAnimatorLayerIndex);
                string lTransition = mController.GetAnimatorStateTransitionName(mAnimatorLayerIndex);

                if (lTransition == lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunLeft135" ||
                    lState == lBaseLayer + "AdventureForward-SM.RunLeft135" ||
                    lTransition == lBaseLayer + "AdventureForward-SM.RunLeft135 -> " + lBaseLayer + "AdventureForward-SM.Run" ||

                    lTransition == lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunLeft180" ||
                    lState == lBaseLayer + "AdventureForward-SM.RunLeft180" ||
                    lTransition == lBaseLayer + "AdventureForward-SM.RunLeft180 -> " + lBaseLayer + "AdventureForward-SM.Run" ||

                    lTransition == lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunRight135" ||
                    lState == lBaseLayer + "AdventureForward-SM.RunRight135" ||
                    lTransition == lBaseLayer + "AdventureForward-SM.RunRight135 -> " + lBaseLayer + "AdventureForward-SM.Run" ||

                    lTransition == lBaseLayer + "AdventureForward-SM.Run -> " + lBaseLayer + "AdventureForward-SM.RunRight180" ||
                    lState == lBaseLayer + "AdventureForward-SM.RunRight180" ||
                    lTransition == lBaseLayer + "AdventureForward-SM.RunRight180 -> " + lBaseLayer + "AdventureForward-SM.Run" ||

                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft90" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft135" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft180" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToRight90" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToRight135" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleToRight180" ||

                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft90" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft135" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToLeft180" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToRight90" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToRight135" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleToRight180" ||

                    lState == lBaseLayer + "AdventureForward-SM.IdleToRight90" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleToRight135" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleToRight180" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleToLeft90" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleToLeft135" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleToLeft180" ||

                    lState == lBaseLayer + "AdventureForward-SM.IdleRotateRight90" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleRotateRight135" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleRotateLeft90" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleRotateLeft135" ||
                    lState == lBaseLayer + "AdventureForward-SM.IdleRotate180" ||

                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft90" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft135" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight90" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight135" ||
                    lTransition == "Entry -> " + lBaseLayer + "AdventureForward-SM.IdleRotate180" ||

                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft90" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateLeft135" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight90" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotateRight135" ||
                    lTransition == "AnyState -> " + lBaseLayer + "AdventureForward-SM.IdleRotate180"

                    )
                {
                    return true;
                }

                return false;
            }
        }
    }
}
