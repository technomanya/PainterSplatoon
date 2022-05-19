using MoreMountains.NiceVibrations;
using Project.Runtime.Scripts.Managers;
using UnityEngine;

namespace Managers
{
    public class VibrationManager : MonoBehaviour
    {
        public bool IsVibrationEnabled
        {
            get
            {
                return _isVibrationEnabled;
            }
            set
            {
                _isVibrationEnabled = value;
                PlayerPrefs.SetInt("isVibrationEnabled", _isVibrationEnabled ? 1 : 0);
            }
        }
        private bool _isVibrationEnabled;
    
        private void Awake()
        {
            GameManager.instance.vibrationManager = this;
            _isVibrationEnabled = PlayerPrefs.GetInt("isVibrationEnabled", 1) == 1;
        }

        public void PlayHaptic()
        {
            _isVibrationEnabled = PlayerPrefs.GetInt("isVibrationEnabled", 1) == 1;
            if(_isVibrationEnabled)
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }
        public void PlayHapticLight()
        {
            _isVibrationEnabled = PlayerPrefs.GetInt("isVibrationEnabled", 1) == 1;
            if(_isVibrationEnabled)
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
        public void PlayHapticStrong()
        {
            _isVibrationEnabled = PlayerPrefs.GetInt("isVibrationEnabled", 1) == 1;
            if(_isVibrationEnabled)
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
    }
}
