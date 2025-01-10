using Hx.Skill;
using UnityEngine;

namespace Hx.Component
{
    public class ComponentSkillManager : MonoBehaviour
    {
        // public static ComponentSkillManager instance;
        // public Dictionary<string, SkillBase> skills = new Dictionary<string, SkillBase>(
        //     ["DashSkill", SkillDash]
        //     );
        
        [HideInInspector] public SkillDash dash;
        [HideInInspector] public SkillClone clone;
        [HideInInspector] public SkillSwordThrow swordThrow;

        private void Awake()
        {
            // if (instance != null) Destroy(gameObject);
            // else instance = this;
            
            dash = GetComponent<SkillDash>();
            clone = GetComponent<SkillClone>();
            swordThrow = GetComponent<SkillSwordThrow>();
        }
    }
}