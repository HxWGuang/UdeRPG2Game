using System.Collections;
using Hx.Utils;
using UnityEngine;

namespace Hx
{
    /// <summary>
    /// 受击闪烁效果组件
    /// </summary>
    public class ComponentHitFlashFx : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;
        [SerializeField] private float flashDuration;
        [SerializeField] private float flashIntensity;
        private Material originalMaterial;
        private Renderer renderer;

        private void Start()
        {
            renderer = gameObject.GetComponentInChildDirectly<Renderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
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
    }
}
