using Hx.Module;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillCloneController : MonoBehaviour
    {
        [SerializeField] private float transitionDuration = 1f;
        [SerializeField] private Color targetColor = Color.clear;
        private SpriteRenderer _spriteRenderer;
        private float transitionTimer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            transitionTimer = transitionDuration;
        }

        private void Update()
        {
            // 在transitionDuration时间内完成颜色到透明的插值
            if (transitionTimer > 0)
            {
                transitionTimer -= Time.deltaTime;
                var t = 1 - transitionTimer / transitionDuration;
                _spriteRenderer.color = Color.Lerp(Color.white, targetColor, t);
            }
            else
            {
                G.skillMgr.clone.objectPool.Return(gameObject);
            }
        }
    }
}