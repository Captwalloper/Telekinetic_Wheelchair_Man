using UnityEngine;

public class Wheelchair : MonoBehaviour {
    public Rigidbody wheelchair;
    public Rigidbody wheel1;
    public Rigidbody wheel2;
    //
    Transform reference
    {
        get
        {
            var top = SteamVR_Render.Top();
            return (top != null) ? top.origin : null;
        }
    }
    //
    private Vector3? lastPlatformPosition = null;

    private void Update()
    {
        if (wheel1.angularVelocity.magnitude != 0 || wheel2.angularVelocity.magnitude != 0)
        {
            wheelchair.transform.position += new Vector3(.05f, 0, 0);
        }

        var platformPosition = wheelchair.transform.position;
        if (lastPlatformPosition.HasValue && reference != null)
        {
            var diff = platformPosition - lastPlatformPosition;
            if (diff.Value.magnitude > 0)
            {
                reference.position += diff.Value;
                lastPlatformPosition = platformPosition;
            }
        }
        else
        {
            lastPlatformPosition = platformPosition;
        }
    }

    private void Roll()
    {
        const float radius = .5f;
        float dist1 = RotationsPerSecond(wheel1.angularVelocity) * Time.deltaTime * DistancePerRotation(radius);
        float dist2 = RotationsPerSecond(wheel2.angularVelocity) * Time.deltaTime * DistancePerRotation(radius);

        float dist = dist1 < dist2 ? dist1 : dist2;

        wheelchair.transform.position += new Vector3(dist, 0, 0);
    }

    private float RotationsPerSecond(Vector3 angularVelocity)
    {
        return angularVelocity.magnitude / (2 * Mathf.PI);
    }

    private float DistancePerRotation(float radius)
    {
        return 2 * Mathf.PI * radius;
    }
}
