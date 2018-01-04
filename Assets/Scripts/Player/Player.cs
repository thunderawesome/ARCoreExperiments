using UnityEngine;
using GoogleARCore;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    #region Public Variables
    public UnityEngine.UI.Text text;
    #endregion

    #region Private Variables
    [SerializeField] private Stats _stats;
    private Animator _animator;
    private NavMeshAgent m_agent;
    #endregion

    #region Private Methods
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        text = GameObject.FindWithTag("Text").GetComponent<UnityEngine.UI.Text>();
    }

    private void Movement()
    {
        if (m_agent.isActiveAndEnabled == true && m_agent.isOnNavMesh == true)
        {
            text.text = "<color=green>AGENT ACTIVE & ON MESH</color>";
            if (m_agent.isStopped == true)
            {
                //animate the player to walk/run
                _animator.SetFloat("Movement", 0);
                text.text = "<color=red>AGENT STOPPED</color>";
            }
            else
            {
                //animate the player to walk/run
                _animator.SetFloat("Movement", m_agent.speed);
                text.text = "<color=green>AGENT MOVING</color>";
            }
        }
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast against the location the player touched to search for planes.            
            if (Session.Raycast(Input.mousePosition.x, Input.mousePosition.y, raycastFilter, out hit))
            {
                Debug.Log("<color=green>DESTINATION SET!</color>");
                text.text = "<color=green>AGENT DESTINATION SET</color>";
                m_agent.destination = hit.Pose.position;
            }
        }

        // If the player has not touched the screen, we are done with this update.
        UnityEngine.Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        if (Session.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            m_agent.destination = hit.Pose.position;
        }
    }

    private void Update()
    {
        Movement();
    }
    #endregion
}

[System.Serializable]
public class Stats
{
    public float Speed;
    public float JumpSpeed;
    public float RotateSpeed;
    public float Gravity;
    public float MaxSpeed;
    public float MinSpeed;
}