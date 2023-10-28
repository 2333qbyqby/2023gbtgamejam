using UnityEngine;
using UnityEngine.UI;
using Audio;
using Unity.VisualScripting;

namespace UI
{
    public class SettingUIManager : MonoBehaviour
    {
        public static SettingUIManager instance { get; private set; }
        [SerializeField, Tooltip("音量滑块")] private Slider voiceSlider;
        [SerializeField, Tooltip("音效滑块")] private Slider sFXSlider;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            //初始化音量
            AudioManager.instance.InitMusicSld();
            AudioManager.instance.InitSoundSld();
            
            voiceSlider.value = PlayerPrefs.GetFloat("CurMusicVolume", 0);
            sFXSlider.value = PlayerPrefs.GetFloat("CurSFXVolume", 0);
        }
        
        /// <summary>
        ///  脚本实际上只是起到了传递参数的作用,因为AudioManager是持续单例，在某些场景中不是初始存在，无法直接挂载到组件上，需要使用代码动态调用
        /// </summary>
        public void MusicSldOnClick()
        {
            AudioManager.instance.MusicSldOnClick(voiceSlider);
            PlayerPrefs.SetFloat("CurMusicVolume",voiceSlider.value);
        }
        public void SoundSldOnClick()
        {
            AudioManager.instance.SoundSldOnClick(sFXSlider);
            PlayerPrefs.SetFloat("CurSFXVolume",sFXSlider.value);
        }
    }
}
