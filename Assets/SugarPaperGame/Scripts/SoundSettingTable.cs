using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SugarpaperGame
{
    [CreateAssetMenu(fileName = "SoundTable", menuName = "ModuGame/SoundTable", order = 0)]
    public class SoundSettingTable : ScriptableObject
    {
        [System.Serializable]
        private class SoundKeyPair
        {
            public string key;
            public AudioClip clip;
        }

        [SerializeField] private SoundKeyPair[] sounds;

        private Dictionary<string, AudioClip> soundMap = new Dictionary<string, AudioClip>();

        public AudioClip GetSound(string key)
        {
            if (soundMap.Count == 0)
            {
                foreach (var sound in sounds)
                {
                    soundMap.Add(sound.key, sound.clip);
                }
            }

            return soundMap.TryGetValue(key, out var clip) ? clip : null;
        }
    }
}