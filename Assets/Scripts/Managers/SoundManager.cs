using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        // ƒVƒ“ƒOƒ‹ƒgƒ“
        public static SoundManager instance;

        public AudioSource auddioSourceBGM;

        public AudioSource audioSourceButtonSE;
        public AudioClip audioButtonClip;

        public AudioSource audioSourceBombSE;
        public AudioClip audioBombClip;

        public AudioSource audioSourceJumpSE;
        public AudioClip audioJumpClip;

        public AudioSource audioSourceSuccessSE;
        public AudioClip audioSuccessClip;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void PlayBGM()
        {
            auddioSourceBGM.Play();
        }

        public void StopBGM()
        {
            auddioSourceBGM.Stop();
        }

        public void TuneBGMVolume(float volume)
        {
            auddioSourceBGM.volume = volume;
        }

        public void PlayButtonSE()
        {
            audioSourceButtonSE.PlayOneShot(audioButtonClip);
        }

        public void PlayBombSE()
        {
            audioSourceBombSE.PlayOneShot(audioBombClip);
        }

        public void PlayJumpSE()
        {
            audioSourceJumpSE.PlayOneShot(audioJumpClip);
        }

        public void PlaySuccessSE()
        {
            audioSourceJumpSE.PlayOneShot(audioSuccessClip);
        }
    }

}
