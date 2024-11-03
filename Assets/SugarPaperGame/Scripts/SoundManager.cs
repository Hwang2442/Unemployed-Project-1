using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SugarpaperGame
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundSettingTable soundTable;
    }
}