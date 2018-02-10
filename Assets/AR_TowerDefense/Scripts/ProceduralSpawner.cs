using GoogleARCore.HelloAR;
using System.Collections;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour
{
    #region Public Variables
    public static ProceduralSpawner Instance;

    public GameObject[] objects;
    public Vector3 position = Vector3.zero;
    #endregion

    #region Private Variables
    private int m_index = 0;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator InitializeObjects(float radius)
    {
        // Check within a spherical radius to see if anything is occupying that space.
        if (Physics.CheckSphere(position, radius))
        {
            // Something is occupying this space. EXIT Coroutine
            yield break;
        }
        else
        {
            try
            {
                // Don't want to exceed the number of objects. EXIT Coroutine.
                if (m_index > objects.Length) yield break;

                // Spot is empty, we can spawn
                objects[m_index].Create(position);

                // Increase the index to start spawning the next object
                m_index++;

                position = new Vector3(Random.Range(position.x, radius), position.y, Random.Range(position.z, radius));
            }
            catch (System.Exception)
            {

                throw new System.Exception("No Objects set up in Array.");
            }           
        }
    }
}
