using Racing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(AudioSource))]
    public class GridBoxSound : MonoBehaviour
    {
        //[SerializeField] private Sounds sounds;
        //[SerializeField] private Car car;

        /*[SerializeField] private float pitchModifier;
        [SerializeField] private float volumeModifier;
        [SerializeField] private float rpmModifier;

        [SerializeField] private float basePitch = 1.0f;
        [SerializeField] private float baseVolume = 0.4f;*/


        private AudioSource gridBoxAudioSource;

        private void Start()
        {
            gridBoxAudioSource = GetComponent<AudioSource>();
        }
        public void GridBoxSoundPlay()
        {
            gridBoxAudioSource.Play();
        }
    }
}