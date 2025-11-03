// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour{

    public Transform bulletImpact; //총알 파편 효과
    ParticleSystem bulletEffect; //총알 파티클 시스템
    AudioSource bulletAudio; //총알 발사 사운드

    public Transform crosshair;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {   ARAVRInput.DrawCrosshair(crosshair);
        //Index --> Bimnary로 봤을 때 같은 자리에 같은 값(1)이 있다면 1로 봄. and일 때는 하나라도 0일 경우 0, or일때는 하나라도 1일 경우 1
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            bulletAudio.Stop();
            bulletAudio.Play();
            
                //Ray를 카메라의 위치로부터 나가도록 만든가.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);

            //Ray의 충돌 정보를 저장하기 위한 변수 지정
            RaycastHit hitInfo;
            //플레이어 레이어 얻어오기
            int playerLayer = 1 << LayerMask.NameToLayer("Player"); //6

            //타워 레이어 얻어오기
            int towerLayer = 1 << LayerMask.NameToLayer("Tower"); //7
            int layerMask = playerLayer | towerLayer;

            //Ray를 쏜다. ray가 부딪힌 정보는 hitinfo에 담긴다.

            if (Physics.Raycast(ray,out hitInfo,200,~layerMask))
            {
                //총알 파편 효과 처리

                //총알 이펙트 진행되고 있으면 멈추고 진행
                bulletEffect.Stop();
                bulletEffect.Play();

                //부딪힌 지점 바로 위에서 이펙트가 보이도록 설정
                bulletImpact.position = hitInfo.point;

                //부딪힌 지점의 방향으로 총알 이펙트의 방향을 설정
                bulletImpact.forward = hitInfo.normal;
            }
        }

    }

}