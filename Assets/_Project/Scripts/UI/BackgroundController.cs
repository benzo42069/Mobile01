// PulseStrike | BackgroundController | Phase 12
using PulseStrike.Cosmetics;
using UnityEngine;

namespace PulseStrike.UI
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private ParticleSystem deepSpaceParticles;

        public void ApplyTheme(BackgroundThemeData theme)
        {
            Camera.main.backgroundColor = theme.backgroundColor;
            backgroundRenderer.sprite = theme.backgroundSprite;
            backgroundRenderer.material = theme.backgroundMaterial;

            if (deepSpaceParticles != null)
            {
                if (theme.themeId == "bg_deepspace")
                {
                    deepSpaceParticles.gameObject.SetActive(true);
                    deepSpaceParticles.Play();
                }
                else
                {
                    deepSpaceParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    deepSpaceParticles.gameObject.SetActive(false);
                }
            }
        }
    }
}
