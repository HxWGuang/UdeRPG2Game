using Hx.Skill;
using UnityEngine;

namespace Hx.Module
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager instance;
        // public Dictionary<string, SkillBase> skills = new Dictionary<string, SkillBase>(
        //     ["DashSkill", SkillDash]
        //     );
        
        [HideInInspector] public SkillDash dash;
        [HideInInspector] public SkillClone clone;

        private void Awake()
        {
            if (instance != null) Destroy(gameObject);
            else instance = this;
            
            dash = GetComponent<SkillDash>();
            clone = GetComponent<SkillClone>();
        }
    }
}