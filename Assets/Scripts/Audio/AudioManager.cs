using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//使用单例模式开发，继承单例父类，使其在场景切换时不销毁
//同时包括音量设置功能和播放音效功能
namespace Audio
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField, Tooltip("混音器")] private AudioMixer audioMixer;
        //改变音调
        const float k_PitchMin = 0.8f;
        const float k_PitchMax = 1.2f;

        //音量设置
        //Slider on click：调节音量
        public void MasterSldOnClick(Slider slider)
        {
            audioMixer.SetFloat("vMaster", slider.value);
        }
        public void MusicSldOnClick(Slider slider)
        {
            audioMixer.SetFloat("vMusic", slider.value);
        }
        public void SoundSldOnClick(Slider slider)
        {
            audioMixer.SetFloat("vSound", slider.value);
        }
        
        public void InitMusicSld()
        {
            audioMixer.SetFloat("vMusic", PlayerPrefs.GetFloat("CurMusicVolume", 0));
        }
        public void InitSoundSld()
        {
            audioMixer.SetFloat("vSound", PlayerPrefs.GetFloat("CurSFXVolume", 0));
        }
        
        [SerializeField] private List<AudioSource> sfxs;
        [SerializeField] private AudioSource bgm;

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="id"></param>
        public void PlaySound(int id)
        {
            if (id < sfxs.Count)
            {
                sfxs[id].pitch = 1;
                sfxs[id].Stop();
                sfxs[id].Play();
            }
        }
 
        /// <summary>
        /// 播放随机音调的音效
        /// </summary>
        /// <param name="id"></param>
        public void PlayRandomSound(int id)
        {
            if (id < sfxs.Count){sfxs[id].pitch = Random.Range(k_PitchMin, k_PitchMax);}
            PlaySound(id);
        }

        /// <summary>
        /// 静音
        /// </summary>
        public void StopBgm()
        {
            bgm.Stop();
        }

        public void PlayBgm()
        {
            bgm.Play();
        }
    }
}
