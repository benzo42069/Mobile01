// PulseStrike | RingController | Phase 2
using System;
using PulseStrike.Cosmetics;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Mechanics
{
    public class RingController : MonoBehaviour
    {
        [SerializeField] public float expandSpeed;
        [SerializeField] public float currentRadius;
        [SerializeField] public bool isGhost;
        [SerializeField] public Color ringColor;
        [SerializeField] public int ringIndex;

        private bool isAlive = true;
        private SpriteRenderer spriteRenderer;

        public event Action OnDestroyed;
        public bool IsAlive => isAlive;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!isAlive)
            {
                return;
            }

            currentRadius += expandSpeed * Time.deltaTime;
            transform.localScale = Vector3.one * (currentRadius * 2f);

            if (currentRadius >= GameplayConstants.MaxRingRadius)
            {
                if (!isGhost)
                {
                    RingMissed();
                }

                Destroy(gameObject);
            }
        }

        public void SetSkin(RingSkinData skin)
        {
            if (skin == null || spriteRenderer == null)
            {
                return;
            }

            spriteRenderer.sprite = skin.ringSprite;
            spriteRenderer.color = skin.primaryColor;
            spriteRenderer.material = skin.ringMaterial;
            ringColor = skin.primaryColor;
        }

        public void SetAlive(bool alive)
        {
            isAlive = alive;
        }

        public void TriggerShatterEffect()
        {
            // TODO: Spawn shatter effect prefab and set particle colors to equipped skin.
        }

        private void RingMissed()
        {
            Core.GameManager.Instance.OnRingEscaped();
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
