using UnityEngine;

namespace Battlerock
{
    public class MultiplayerManager : MonoBehaviour
    {
        #region Public Variables
        public static MultiplayerManager Instance;

        /// <summary>
        /// Anchor point used by the player.
        /// </summary>
        public GoogleARCore.Anchor anchor;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before anything else)
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }
        #endregion
    }
}