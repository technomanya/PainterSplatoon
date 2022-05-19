using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class ShootingSystem : MonoBehaviour
{

    MovementInput input;

    [SerializeField] ParticleSystem inkParticle;
    [SerializeField] Transform parentController;
    [SerializeField] Transform splatGunNozzle;
    [SerializeField] CinemachineFreeLook freeLookCamera;
    CinemachineImpulseSource impulseSource;

    
    public float currPaintAmount = 10f;
    public float maxPaintAmount = 10f;

    [SerializeField] private bool isPaintFilling = false;

    void Start()
    {
        input = GetComponent<MovementInput>();
        impulseSource = freeLookCamera.GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        //Debug.Log(gameObject.name + ": " + currPaintAmount);
        Vector3 angle = parentController.localEulerAngles;
        if(currPaintAmount > 0)
        {
            inkParticle.Play();
            currPaintAmount -= Time.deltaTime;
        }
        else
        {
            inkParticle.Stop();
        }
        
        /*if (isPaintFilling && currPaintAmount <= maxPaintAmount)
        {
            currPaintAmount += Time.deltaTime*3;
        }*/
        
        
        //input.blockRotationPlayer = Input.GetMouseButton(0);
        /*bool pressing = Input.GetMouseButton(0);

        if (Input.GetMouseButton(0))
        {
            VisualPolish();
            input.RotateToCamera(transform);
        }

        if (Input.GetMouseButtonDown(0))
            inkParticle.Play();
        else if (Input.GetMouseButtonUp(0))
            inkParticle.Stop();*/

        /*parentController.localEulerAngles
            = new Vector3(Mathf.LerpAngle(parentController.localEulerAngles.x, pressing ? RemapCamera(freeLookCamera.m_YAxis.Value, 0, 1, -25, 25) : 0, .3f), angle.y, angle.z);*/
    }

    void VisualPolish()
    {
        if (!DOTween.IsTweening(parentController))
        {
            parentController.DOComplete();
            Vector3 forward = -parentController.forward;
            Vector3 localPos = parentController.localPosition;
            parentController.DOLocalMove(localPos - new Vector3(0, 0, .2f), .03f)
                .OnComplete(() => parentController.DOLocalMove(localPos, .1f).SetEase(Ease.OutSine));

           impulseSource.GenerateImpulse();
        }

        if (!DOTween.IsTweening(splatGunNozzle))
        {
            splatGunNozzle.DOComplete();
            splatGunNozzle.DOPunchScale(new Vector3(0, 1, 1) / 1.5f, .15f, 10, 1);
        }
    }

    float RemapCamera(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PaintSource") && currPaintAmount <= maxPaintAmount)
        {
            currPaintAmount += Time.deltaTime*3;
        }
    }
}
