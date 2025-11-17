using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCurve : MonoBehaviour
{   

    public Transform teleportCircleUI;

    LineRenderer lr;

    Vector3 originScale = Vector3.one * 0.02f;

    public int lineSmooth = 40;

    public float curveLength  =50;

    public float gravity = -60;
    public float simulateTime = 0.02f;

    List<Vector3> lines = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        teleportCircleUI.gameObject.SetActive(false);

        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.0f;
        lr.endWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            lr.enabled = true;
        }
        else if (ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {   
            //내가 그린 커브 보려면 아래 줄 주석 처리
            lr.enabled = false;

            if(teleportCircleUI.gameObject.activeSelf)
            {   

                //동시 접근을 막기 위해 false처리
                GetComponent<CharacterController>().enabled = false;
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }

            teleportCircleUI.gameObject.SetActive(false);
        }

        else if (ARAVRInput.Get(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            MakeLines();
        }
    }

    void MakeLines()
    {
        lines.RemoveRange(0,lines.Count);
        Vector3 dir = ARAVRInput.RHandDirection * curveLength;
        Vector3 pos = ARAVRInput.RHandPosition;

        lines.Add(pos);


        for(int i = 0; i<lineSmooth;i++)
        {
            Vector3 lastPos = pos;
            dir.y += gravity * simulateTime;
            pos += dir * simulateTime;

            if(CheckHitRay(lastPos, ref pos))
            {
                lines.Add(pos);
                break;
            }
            else
            {
                teleportCircleUI.gameObject.SetActive(false);
            }

            lines.Add(pos);
        }

        lr.positionCount = lines.Count;
        lr.SetPositions(lines.ToArray());
    }

    private bool CheckHitRay(Vector3 lastPos, ref Vector3 pos)
    {
        Vector3 rayDir  = pos - lastPos;
        Ray ray = new Ray(lastPos, rayDir);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, rayDir.magnitude))
        {
            pos = hitInfo.point;
            int layer = LayerMask.NameToLayer("Terrain");

            if (hitInfo.transform.gameObject.layer == layer)
            {
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = pos;
                teleportCircleUI.forward = hitInfo.normal;

                float distance = (pos - ARAVRInput.RHandPosition).magnitude;

                teleportCircleUI.localScale = originScale * Mathf.Max(1, distance);
            }

            return true;
        }
        return false;
    }
}
