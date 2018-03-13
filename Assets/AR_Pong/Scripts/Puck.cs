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
            if (photonView.isMine == true)
            {
                if (collision.gameObject.tag == "Player")
                {
                    ContactPoint cp = collision.contacts[0];
                    // calculate with addition of normal vector
                    // myRigidbody.velocity = oldVel + cp.normal*2.0f*oldVel.magnitude;

                    // calculate with Vector3.Reflect
                    m_rigidbody.velocity = Vector3.Reflect(m_rigidbody.velocity, cp.normal);

                    // bumper effect to speed up ball
                    m_rigidbody.velocity += cp.normal * speed;

                    //m_color = collision.gameObject.GetComponent<PlayerManager>().playerColor;
                    //Vector3 color = MultiplayerManager.Instance.LocalPlayer.GetColor();

                    //foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                    //{
                    //    rend.material.color = new Color(color.x, color.y, color.z, 1);
                    //}
                    //photonView.RPC("SetColor", PhotonTargets.AllBuffered, m_color.r, m_color.g, m_color.b);
                }
            }

            if (collision.gameObject.tag == "P1_Goal")
            {
                PhotonNetwork.playerList[1].AddScore(1);
            }
            else if (collision.gameObject.tag == "P2_Goal")
            {
                PhotonNetwork.playerList[0].AddScore(1);
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