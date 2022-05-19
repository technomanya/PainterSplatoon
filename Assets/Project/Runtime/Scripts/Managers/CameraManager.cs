using System;
using Controllers;
using Misc;
using Project.Runtime.Scripts.Managers;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [HideInInspector] public Camera mainCamera;
        public CameraEffects cameraEffects;

        private void Awake()
        {
            mainCamera = Camera.main;
            GameManager.instance.cameraManager = this;
        }

        public void CameraShake()
        {
            cameraEffects.Shake();
        }
    }
}
