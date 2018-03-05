using UnityEngine;

namespace Battlerock
{
    public class MultiplayerManager : MonoBehaviour
    {
        #region Public Variables
        public static MultiplayerManager Instance;
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