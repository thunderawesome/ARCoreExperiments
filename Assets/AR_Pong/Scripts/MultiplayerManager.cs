using Photon;
using UnityEngine;

namespace Battlerock
{
    public class MultiplayerManager : PunBehaviour
    {
        #region Public Variables
        public static MultiplayerManager Instance;

        public bool isReady = false;

        public GoogleARCore.Anchor Anchor
        {
            get
            {
                if (m_anchorPoint == null)
                {
                    m_anchorPoint = FindObjectOfType<GoogleARCore.Anchor>();

                    if (m_anchorPoint == null)
                    {
                        Debug.Log("<color=red>No Anchor found in scene.</color>");
                        Pose pose = new Pose
                        {
                            position = Vector3.zero,
                            rotation = Quaternion.identity
                        };
                        m_anchorPoint = GoogleARCore.Session.CreateAnchor(pose);
                        if (m_anchorPoint == null)
                        {
                            Debug.LogError("Failed to create an anchor.");
                            return null;
                        }
                        else
                        {
                            return m_anchorPoint;
                        }

                    }
                    else
                    {
                        Debug.Log("<color=green>Found Anchor in scene.</color>");
                        return m_anchorPoint;
                    }
                }
                else
                {
                    Debug.Log("<color=blue>Anchor already exists in scene.</color>");
                    return m_anchorPoint;
                }
            }
        }
        #endregion

        #region Private Variables
        /// <summary>
        /// Anchor point used by the player.
        /// </summary>
        private GoogleARCore.Anchor m_anchorPoint;
        #endregion

        #region Private Variables
        private GameObject m_placeHolder;
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

        #region Public Methods
        public void FindOrCreateAnchor(Transform parent = null)
        {
            if (m_placeHolder == null)
            {
                m_placeHolder = PhotonNetwork.Instantiate("Anchor", Vector3.zero, Quaternion.identity, 0);
                Debug.Log(m_placeHolder.name + " created!");
                m_placeHolder.transform.parent = parent;
                m_placeHolder.transform.localPosition = Vector3.zero;
                DontDestroyOnLoad(m_placeHolder);
               
            }

            if (_GameManager.Instance.isARCoreEnabled == true)
            {
                Anchor.transform.parent = m_placeHolder.transform;
                Anchor.transform.localPosition = Vector3.zero;
            }
        }

        public void PrepareMultiplayer(Transform parent = null)
        {
            if (isReady == true) return;
            
            SpawnManager.Instance.SpawnPlayer(parent);

            isReady = true;
        }
        #endregion
    }
}