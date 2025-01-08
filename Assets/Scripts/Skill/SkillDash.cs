using Hx.Utils;

namespace Hx.Skill
{
    public class SkillDash : SkillBase
    {
        public override void UseSkill()
        {
            base.UseSkill();
            
            LogUtils.Log("Use Dash Skill");
        }
    }
}