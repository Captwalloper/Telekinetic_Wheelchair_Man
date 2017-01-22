using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public GameObject movingPlatform;
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

    private void FixedUpdate()
    {
        var platformPosition = movingPlatform.transform.position;
        if (lastPlatformPosition.HasValue && reference != null)
        {
            var diff = platformPosition - lastPlatformPosition;
            reference.position += diff.Value;
        }
        lastPlatformPosition = platformPosition;
    }
}
