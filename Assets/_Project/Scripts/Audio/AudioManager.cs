// PulseStrike | AudioManager | Phase 9
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource audioSourceSFX;
        [SerializeField] private AudioSource audioSourceMusic;
        [SerializeField] private AudioClip[] hitClips;
        [SerializeField] private AudioClip missClip;
        [SerializeField] private AudioClip comboBreakClip;
        [SerializeField] private AudioClip uiClickClip;
        [SerializeField] private AudioClip purchaseSuccessClip;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            InitializeVolumes();
        }

        public void InitializeVolumes()
        {
            var sfxVolume = SaveSystem.GetSfxVolume();
            var musicVolume = SaveSystem.GetMusicVolume();
            if (audioSourceSFX != null) audioSourceSFX.volume = sfxVolume;
            if (audioSourceMusic != null) audioSourceMusic.volume = musicVolume;
        }

        public void PlayHitSound(int combo)
        {
            if (audioSourceSFX == null || hitClips == null || hitClips.Length == 0)
            {
                return;
            }

            audioSourceSFX.pitch = Progression.ComboTracker.Instance.GetComboAudioPitch();
            audioSourceSFX.PlayOneShot(hitClips[Random.Range(0, hitClips.Length)]);
        }

        public void PlayMissSound()
        {
            if (audioSourceSFX != null && missClip != null)
            {
                audioSourceSFX.pitch = 1f;
                audioSourceSFX.PlayOneShot(missClip);
            }
        }

        public void PlayComboBreakSound()
        {
            if (audioSourceSFX != null && comboBreakClip != null)
            {
                audioSourceSFX.pitch = 1f;
                audioSourceSFX.PlayOneShot(comboBreakClip);
            }
        }

        public void PlayUIButtonSound()
        {
            if (audioSourceSFX != null && uiClickClip != null)
            {
                audioSourceSFX.PlayOneShot(uiClickClip);
            }
        }

        public void PlayPurchaseSuccessSound()
        {
            if (audioSourceSFX != null && purchaseSuccessClip != null)
            {
                audioSourceSFX.PlayOneShot(purchaseSuccessClip);
            }
        }
    }
}
