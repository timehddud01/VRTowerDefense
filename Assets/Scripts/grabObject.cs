using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObject : MonoBehaviour
{   
    bool isGrabbing = false;
    GameObject grabbedObject; 
    public LayerMask grabbedLayer;
    public float grabRange = 0.5f;

    Vector3 prevPos ;
    float throwPower = 1000;

    //각속도
    //4차원...
    Quaternion prevRot;
    public float rotPower = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbing == false)
        {
            TryGrab();
        }
        else
        {
            TryUnGrab();
        }
    }

    private void TryGrab()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))//텔러포트랑 겹쳐서 거기를 주석 처리
        {
            Collider[] hitObjects = Physics.OverlapSphere(ARAVRInput.RHandPosition,grabRange, grabbedLayer);

            int closest = -1;
            float closestDistance = float.MaxValue;
            for (int i = 0; i < hitObjects.Length; i++)
            {
                var rigid = hitObjects[i].GetComponent<Rigidbody>();
                if (rigid == null) 
                {
                    continue;
                }

                Vector3 nextPos = hitObjects[i].transform.position;
                float nextDistance = Vector3.Distance(nextPos, ARAVRInput.RHandPosition);

                if (nextDistance < closestDistance)
                {
                    closest = i ;
                    closestDistance = nextDistance;
                }

                if (closest > -1)
                {
                    isGrabbing = true ; 
                    grabbedObject = hitObjects[closest].gameObject;
                    grabbedObject.transform.parent = ARAVRInput.RHand;


                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

                    prevPos = ARAVRInput.RHandPosition;

                    prevRot = ARAVRInput.RHand.rotation;
                }
            }
        }
    }


    private void TryUnGrab()
    {
        Vector3 throwDirection = (ARAVRInput.RHandPosition - prevPos);

        prevPos = ARAVRInput.RHandPosition;


        //쿼터니온 공식
        Quaternion deltaRoatation = ARAVRInput.RHand.rotation * Quaternion.Inverse(prevRot);
        prevRot = ARAVRInput.RHand.rotation;

        if (ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            isGrabbing = false;

            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            grabbedObject.transform.parent = null;

            grabbedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwPower; // linearVelocity안되면 velocity 만 사용해도 됨
            
            float angle;
            Vector3 axis;
            deltaRoatation.ToAngleAxis(out angle, out axis);
            Vector3 angularVelocity = (1.0f /Time.deltaTime) *angle *axis;
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = angularVelocity;
            
            grabbedObject = null;
        }
    }
}
