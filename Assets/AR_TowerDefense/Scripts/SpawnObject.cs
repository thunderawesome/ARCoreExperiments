using GoogleARCore;
using UnityEngine;

public static class SpawnObject
{
    public static void Create(this GameObject obj, Touch touch, Vector2? offset = null)
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

        if(offset != null)
        {
            touch.position -= (Vector2)offset;
        }

        // Raycast against the location the player touched to search for planes.
        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            Debug.Log("Creating object at raycast hit location based on TOUCH.");
            AnchorObject(obj, hit);
        }
    }

    public static void Create(this GameObject obj, Vector3 position, bool canRaycast = true)
    {
        if (canRaycast == true)
        {
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;
            // Raycast against the location to search for planes.
            if (Frame.Raycast(position.x, position.y, raycastFilter, out hit))
            {
                Debug.Log("Creating object at raycast hit location based on POSITION.");
                AnchorObject(obj, hit);
            }
        }
        else
        {
            GameObject go = GameObject.Instantiate(obj, position, Quaternion.identity);
            Debug.Log("GameObject -> " + go.name + " <- created.");
        }
    }

    private static void AnchorObject(this GameObject obj, TrackableHit hit, Transform lookAt = null)
    {
        GameObject go = GameObject.Instantiate(obj, hit.Pose.position, hit.Pose.rotation);
        Debug.Log("GameObject -> " + go.name + " <- created.");

        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
        // world evolves.
        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
        GoogleARCore.Battlerock.ARController.Instance.anchor = anchor;
        Debug.Log("Anchor created in world.");

        if (lookAt != null)
        {
            // Spawned object should look at the camera but still be flush with the plane.
            go.transform.LookAt(lookAt);

            go.transform.rotation = Quaternion.Euler(0.0f,
                go.transform.rotation.eulerAngles.y, go.transform.rotation.z);

            Debug.Log(go.name + " is looking at camera.");
        }

        // Make spawned model a child of the anchor.
        go.transform.parent = anchor.transform;
    }
}
