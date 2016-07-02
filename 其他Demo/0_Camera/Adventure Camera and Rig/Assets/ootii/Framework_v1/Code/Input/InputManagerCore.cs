using System;
using System.Collections;
using UnityEngine;

namespace com.ootii.Input
{
    /// <summary>
    /// Used by the InputManager to hook into the unity update process. This allows us
    /// to update the input and track old values
    /// </summary>
    [Serializable]
    public class InputManagerCore : MonoBehaviour
    {
        /// <summary>
        /// Saves state for the Xbox controller usage
        /// </summary>
        public bool IsXboxControllerEnabled = true;

        /// <summary>
        /// Raised first when the object comes into existance. Called
        /// even if script is not enabled.
        /// </summary>
        void Awake()
        {
            // Don't destroyed automatically when loading a new scene
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Called after the Awake() and before any update is called.
        /// </summary>
        public IEnumerator Start()
        {
            // Initialize the manager
            InputManager.Initialize();

            // Create the coroutine here so we don't re-create over and over
            WaitForEndOfFrame lWaitForEndOfFrame = new WaitForEndOfFrame();

            // Loop endlessly so we can process the input
            // at the end of each frame, preparing for the next
            while (true)
            {
                yield return lWaitForEndOfFrame;
                InputManager.Update();
            }
        }

        /// <summary>
        /// Called when the InputManager is disabled. We use this to
        /// clean up objects that were created.
        /// </summary>
        public void OnDisable()
        {
        }
    }
}
