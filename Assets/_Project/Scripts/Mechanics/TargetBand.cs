// PulseStrike | TargetBand | Phase 2
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Mechanics
{
    public class TargetBand : Singleton<TargetBand>
    {
        public float bandRadius = GameplayConstants.InitialBandRadius;
        public float bandWidth = GameplayConstants.InitialBandWidth;
        [SerializeField] private SpriteRenderer bandRenderer;

        protected override void Awake()
        {
            base.Awake();
            if (bandRenderer == null)
            {
                bandRenderer = GetComponent<SpriteRenderer>();
            }

            if (bandRenderer != null)
            {
                bandRenderer.color = Colors.Accent3;
            }
        }

        public void SetBandWidth(float width)
        {
            bandWidth = width;
            transform.localScale = Vector3.one * bandRadius * 2f;
        }
    }
}
