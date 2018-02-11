using UnityEngine;

public class ProceduralSpawner : MonoBehaviour
{
    #region Public Variables
    public static ProceduralSpawner Instance;

    public LayerMask areaCheckingLayerMask;

    public GameObject[] objects;

    public bool canBeginSpawning = false;

    #endregion

    #region Private Variables
    private int m_index = 0;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public void InitializeObject(Vector3 position)
    {
        if (m_index >= objects.Length) return;

        // Don't bother spawning anything unless we get the OK
        if (canBeginSpawning == false) return;

        Renderer renderer = objects[m_index].GetComponent<Renderer>();
        float radius = renderer.bounds.extents.x > renderer.bounds.extents.z ? renderer.bounds.extents.x : renderer.bounds.extents.z;
        // Check within a spherical radius to see if anything is occupying that space.
        if (Physics.CheckSphere(position, radius / 2, areaCheckingLayerMask))
        {
            // Something is occupying this space. EXIT Coroutine
            //Debug.Log("Area is occupied. Cannot spawn object.");
            return;
        }
        else
        {
            try
            {
                // Spot is empty, we can spawn
                objects[m_index].Create(position, false);

                // Increase the index to start spawning the next object
                m_index++;

                //Debug.Log("Object created and index increased: " + objects[m_index].name + "(" + m_index + ")");
            }
            catch (System.Exception)
            {
                throw new System.Exception("No Objects set up in Array.");
            }
        }
    }
}
