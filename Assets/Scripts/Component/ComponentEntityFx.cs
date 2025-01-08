using System.Collections;
using Hx.Utils;
using UnityEngine;

namespace Hx
{
    /// <summary>
    /// 受击闪烁效果组件
    /// </summary>
    public class ComponentEntityFx : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;
        [SerializeField] private float flashDuration;
        [SerializeField] private float flashIntensity;
        [HideInInspector] public Color blinkColor;
        private Material originalMaterial;
        private Color originalColor;
        private Renderer fxRenderer;

        private void Start()
        {
            fxRenderer = gameObject.GetComponentInChildDirectly<Renderer>();
            if (fxRenderer != null)
            {
                originalMaterial = fxRenderer.material;
                originalColor = fxRenderer.material.color;
            }
        }

        public void PlayFlashFx()
        {
            StartCoroutine(nameof(DoFlash));
        }

        private IEnumerator DoFlash()
        {
            fxRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            fxRenderer.material = originalMaterial;
        }

        private void Blink()
        {
            if (fxRenderer.material.color != blinkColor)
            {
                fxRenderer.material.color = blinkColor;
            } else if (fxRenderer.material.color == blinkColor)
            {
                fxRenderer.material.color = originalColor;
            }
        }

        private void StopBlink()
        {
            CancelInvoke("Blink");
            fxRenderer.material = originalMaterial;
            fxRenderer.material.color = originalColor;
        }
    }
}
