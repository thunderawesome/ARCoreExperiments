namespace Battlerock
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Puck : Photon.PunBehaviour
    {
        #region Public Variables
        public float speed = 10f;
        #endregion

        #region Private Variables
        private Rigidbody m_rigidbody;
        private Color m_color;
        #endregion

        #region Unity Methods
        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            AddRandomForce(Random.Range(0, 2));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                m_color = collision.gameObject.GetComponent<PlayerManager>().playerColor;
                photonView.RPC("SetColor", PhotonTargets.AllBuffered, m_color.r, m_color.g, m_color.b);
            }
        }
        #endregion

        #region Private Methods
        private void AddRandomForce(int value)
        {
            Vector3 force = value < 1 ? Vector3.forward : Vector3.back;
            m_rigidbody.AddForce(force * speed, ForceMode.Impulse);
        }

        /// <summary>
        /// Updates the color across the network so all players can see which color the other players are.
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        [PunRPC]
        private void SetColor(float r, float g, float b)
        {
            //set the Game objects underneath the Player like the paddle to the players color
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.material.color = new Color(r, g, b, 1);
            }
        }
        #endregion
    }
}