using UnityEngine;

namespace Hx.Module
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance;

        private void Awake()
        {
            if (instance != null) Destroy(gameObject);
            else instance = this;
        }
    }
}