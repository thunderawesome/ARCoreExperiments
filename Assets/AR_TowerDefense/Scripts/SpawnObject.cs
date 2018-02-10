using GoogleARCore;
using UnityEngine;

public static class SpawnObject
{
    public static void Create(this GameObject obj, Touch touch)
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;
        // Raycast against the location the player touched to search for planes.
        if (Session.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            AnchorObject(obj, hit);
        }
    }

    public static void Create(this GameObject obj, Vector3 position)
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;
        // Raycast against the location to search for planes.
        if (Session.Raycast(position.x, position.y, raycastFilter, out hit))
        {
            AnchorObject(obj, hit);
        }
    }

    private static void AnchorObject(this GameObject obj, TrackableHit hit, Transform lookAt = null)
    {
        GameObject go = GameObject.Instantiate(obj, hit.Pose.position, hit.Pose.rotation);

        ProceduralSpawner.Instance.position = hit.Pose.position;
        ProceduralSpawner.Instance.StartCoroutine(ProceduralSpawner.Instance.InitializeObjects(.1f));

        // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
        // world evolves.
        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

        if (lookAt != null)
        {
            // Spawned object should look at the camera but still be flush with the plane.
            go.transform.LookAt(lookAt);

            go.transform.rotation = Quaternion.Euler(0.0f,
                go.transform.rotation.eulerAngles.y, go.transform.rotation.z);
        }

        // Make spawned model a child of the anchor.
        go.transform.parent = anchor.transform;
    }
}
