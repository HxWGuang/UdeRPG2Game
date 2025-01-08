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
        private Renderer renderer;

        private void Start()
        {
            renderer = gameObject.GetComponentInChildDirectly<Renderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
                originalColor = renderer.material.color;
            }
        }

        public void PlayFlashFx()
        {
            StartCoroutine(nameof(DoFlash));
        }

        private IEnumerator DoFlash()
        {
            renderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            renderer.material = originalMaterial;
        }

        private void Blink()
        {
            if (renderer.material.color != blinkColor)
            {
                renderer.material.color = blinkColor;
            } else if (renderer.material.color == blinkColor)
            {
                renderer.material.color = originalColor;
            }
        }

        private void StopBlink()
        {
            CancelInvoke("Blink");
            renderer.material = originalMaterial;
            renderer.material.color = originalColor;
        }
    }
}
