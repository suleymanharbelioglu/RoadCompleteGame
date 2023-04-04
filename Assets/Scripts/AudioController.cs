using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance{set;  get; }

    private void Awake() {
        instance = this;
    }
    public AudioSource _collectStartAudio;
    public AudioSource _collectDiamondAudio;
    public AudioSource _gameOverAudio;
    public AudioSource _destroyObstacleAudio;
   
    
}
