using DG.Tweening;
using UnityEngine;

namespace Misc
{
    public class CameraEffects : MonoBehaviour
    {

        public enum ShakeType
        {
            Strong,
            Mild
        }
        private Camera _camera;
        private Transform _cameraTransform;
        private Cinemachine.CinemachineVirtualCamera _cinemachine;
        private Cinemachine.CinemachineBasicMultiChannelPerlin _shakeComponent;

        // Component Constants
        private float defaultZoomValue;

        // Tween Addresses
        private Tween shakeTween, zoomTween;
   
        void Awake()
        {
            //GameController.Instance.cameraEffects = this;
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _cinemachine = _cameraTransform.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
            _shakeComponent = _cinemachine.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            _shakeComponent.m_AmplitudeGain = 0;
            defaultZoomValue = _cinemachine.m_Lens.FieldOfView;
        }
    
        public void Shake(ShakeType shakeType = ShakeType.Strong)
        {
            DOTween.Kill(shakeTween);
            if (shakeType == ShakeType.Strong)
            {
                _shakeComponent.m_AmplitudeGain = 2.5f;
            }
            else if (shakeType == ShakeType.Mild)
            {
                _shakeComponent.m_AmplitudeGain = 1f;
            }
            shakeTween = DOTween.To(() => _shakeComponent.m_AmplitudeGain, x => _shakeComponent.m_AmplitudeGain = x, 0, .1f).SetEase(Ease.InCubic);
        }

        public void Zoom(int zoomValue)
        {
            DOTween.Kill(zoomTween);
            zoomTween = DOTween.To(() => _cinemachine.m_Lens.FieldOfView, x => _cinemachine.m_Lens.FieldOfView = x, defaultZoomValue + zoomValue, .2f).SetEase(Ease.InCubic);
        }
    }
}
