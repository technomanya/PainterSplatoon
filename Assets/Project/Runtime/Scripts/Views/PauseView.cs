using Managers;
using Project.Runtime.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;
using  UnityEngine.UI;

namespace Views
{
    public class PauseView : ViewBaseBehaviour
    {
        [SerializeField] private Button settingsRestartButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle vibrationToggle;
        
        public Button SettingsRestartButton => settingsRestartButton;
        public Button ContinueButton => continueButton;

        public Toggle SoundToggle => soundToggle;

        public Toggle VibrationToggle => vibrationToggle;
        // Start is called before the first frame update
        void Start()
        {
            SoundToggle.isOn = GameManager.instance.audioManager.IsSFXEnabled;
            vibrationToggle.isOn = GameManager.instance.vibrationManager.IsVibrationEnabled;
            
            SettingsRestartButton.onClick.AddListener(RestartButtonAction);
            ContinueButton.onClick.AddListener(ContinueButtonAction);
            SoundToggle.onValueChanged.AddListener(SoundToggleOnChanged(SoundToggle.isOn));
            VibrationToggle.onValueChanged.AddListener(VibrationToggleOnChanged(vibrationToggle.isOn));
        }

        void ContinueButtonAction()
        {
            Close();
        }

        void RestartButtonAction()
        {
            GameManager.instance.RestartGame();
        }

        UnityAction<bool> SoundToggleOnChanged(bool isMute)
        {
            GameManager.instance.SoundControl(soundToggle.isOn);
            return null;
        }
        
        UnityAction<bool> VibrationToggleOnChanged(bool vib)
        {
            GameManager.instance.VibrationControl(vibrationToggle.isOn);
            return null;
        }
    }
}
