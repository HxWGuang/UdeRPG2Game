using Hx.Module;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillClone : SkillBase
    {
        [SerializeField] private GameObject _clonePrefab;

        public override void UseSkill()
        {
            base.UseSkill();
            var clone = Instantiate(_clonePrefab, G.player.transform.position, G.player.transform.rotation);
        }
    }
}