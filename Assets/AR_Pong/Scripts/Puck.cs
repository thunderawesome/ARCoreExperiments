using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Puck : MonoBehaviour
{
    #region Public Variables
    public float speed = 10f;
    #endregion

    #region Private Variables
    private Rigidbody m_rigidbody;
    #endregion

    #region Unity Methods
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        AddRandomForce(Random.Range(0, 2));
    }
    #endregion

    #region Private Methods
    private void AddRandomForce(int value)
    {
        Vector3 force = value < 1 ? Vector3.forward : Vector3.back;
        m_rigidbody.AddForce(force, ForceMode.Impulse);
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
