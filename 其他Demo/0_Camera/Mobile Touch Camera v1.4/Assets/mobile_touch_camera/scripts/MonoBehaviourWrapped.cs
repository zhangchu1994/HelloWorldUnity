// /************************************************************
// *                                                           *
// *   Mobile Touch Camera                                     *
// *                                                           *
// *   Created 2015 by BitBender Games                         *
// *                                                           *
// *   bitbendergames@gmail.com                                *
// *                                                           *
// ************************************************************/

using UnityEngine;

namespace BitBenderGames {

  public class MonoBehaviourWrapped : MonoBehaviour {

    private Transform cachedTransform = null;

    public Transform Transform {
      get {
        if (cachedTransform == null) {
          cachedTransform = transform;
        }
        return cachedTransform;
      }
    }

    private GameObject cachedGO = null;

    public GameObject GameObject {
      get {
        if (cachedGO == null) {
          cachedGO = gameObject;
        }
        return cachedGO;
      }
    }
  }
}
