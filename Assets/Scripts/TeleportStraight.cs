using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{   

    //텔레포트를 표시할 UI
    public Transform teleportCircleUI;
    //선을 그릴 라인 렌더러
    LineRenderer lr;

    Vector3 originScale = Vector3.one*0.02f;

    // Start is called before the first frame update
    void Start()
    {   
        //시작할 때 비활성화한다.
        teleportCircleUI.gameObject.SetActive(false);
        //라인 렌더러 컴포넌트 얻어오기
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        // if(ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        // {
        //     //텔레포트 UI 그리기
        //     lr.enabled = true; 
            
        // }

        // else if(ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        // {
        //     //텔레포트 UI 그리기
        //     // lr.enabled = false;

        //     if(teleportCircleUI.gameObject.activeSelf)
        //     {   

        //         //동시 접근을 막기 위해 false처리
        //         GetComponent<CharacterController>().enabled = false;
        //         transform.position = teleportCircleUI.position + Vector3.up;
        //         GetComponent<CharacterController>().enabled = true;
        //     }

        //     teleportCircleUI.gameObject.SetActive(false);
            
        // }
        // //왼쪽 컨트롤러의 [one] 버튼을 누르고 있을 때
        // else if (ARAVRInput.Get(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        // {
        //     //텔레포트 UI 그리기
        //     //
        //     Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        //     RaycastHit hitInfo;
        //     int layer = 1 << LayerMask.NameToLayer("Terrain");

        //     if(Physics.Raycast(ray, out hitInfo, 200, layer))
        //     {
        //         //부딪힌 지점에 텔레포트 UI 표시
        //         lr.SetPosition(0,ray.origin);
        //         lr.SetPosition(1, hitInfo.point);

        //         teleportCircleUI.gameObject.SetActive(true);
        //         teleportCircleUI.position = hitInfo.point;

        //         teleportCircleUI.forward = hitInfo.normal;

        //         teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
        //     }
        //     else
        //     {
        //         lr.SetPosition(0,ray.origin);
        //         lr.SetPosition(1,ray.origin + ARAVRInput.LHandDirection *200);

        //         teleportCircleUI.gameObject.SetActive(false);
        //     }
        // }
    }
}
