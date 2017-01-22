using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinning : MonoBehaviour {
    public GameObject prefab;
    public Rigidbody attachPoint;

    SteamVR_TrackedObject trackedObj;
    HingeJoint joint;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        //if (joint!=null)
        //{
        //    Debug.Log("Torque :" + joint.currentTorque);
        //}
        
        if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //var go = GameObject.Instantiate(prefab);
            //prefab.SetActive(false);
            //go.transform.position = prefab.transform.position;

            joint = prefab.AddComponent<HingeJoint>();
            joint.connectedBody = attachPoint;
            //joint.breakTorque = 4000f;
        }
        else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            //Object.Destroy(go, 15.0f);
            //prefab.SetActive(true);

            // We should probably apply the offset between trackedObj.transform.position
            // and device.transform.pos to insert into the physics sim at the correct
            // location, however, we would then want to predict ahead the visual representation
            // by the same amount we are predicting our render poses.

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            Vector3 padVector = new Vector3(5, 5, -5);
            if (origin != null)
            {
                //rigidbody.velocity = origin.TransformVector(device.velocity);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity) + padVector;
            }
            else
            {
                //rigidbody.velocity = device.velocity;
                rigidbody.angularVelocity = device.angularVelocity + padVector;
            }
            Debug.Log("Angular Velocity: " + rigidbody.angularVelocity);

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
        }
    }
}
