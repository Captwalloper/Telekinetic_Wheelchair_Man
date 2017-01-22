using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Rigidbody movingPlatform;
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
        movingPlatform.transform.position += new Vector3(.05f, 0, 0);
        var platformPosition = movingPlatform.transform.position;
        if (lastPlatformPosition.HasValue && reference != null)
        {
            var diff = platformPosition - lastPlatformPosition;
            if (diff.Value.magnitude > 0)
            {
                reference.position += diff.Value;
                lastPlatformPosition = platformPosition;
            }
        } else
        {
            lastPlatformPosition = platformPosition;
        }
    }
}
