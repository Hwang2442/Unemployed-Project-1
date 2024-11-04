using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SugarpaperGame
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;

        [SerializeField] private SoundSettingTable soundTable;
        [SerializeField] private AudioSource audioSource;

        public static SoundManager Instance 
        { 
            get 
            {
                if (instance == null)
                    instance = FindObjectOfType<SoundManager>();
                return instance; 
            } 
        }

        private void Awake()
        {
            if (Instance != this)
                Destroy(gameObject);
        }

        public void PlayBGM(string key, float vol = 1f)
        {
            var clip = soundTable.GetSound(key);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.loop = true;
                audioSource.volume = vol;
                audioSource.Play();
            }
        }

        public void PlaySFX(string key, float vol = 1f)
        {
            var clip = soundTable.GetSound(key);
            if (clip != null)
            {
                audioSource.PlayOneShot(clip, vol);
            }
        }

        public void Stop()
        {
            if (audioSource.clip != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}