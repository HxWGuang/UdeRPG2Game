using Hx.Module;
using Hx.Utils;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillSwordThrow : SkillBase
    {
        [SerializeField] private GameObject swordPrefab;
        private ObjectPool<GameObject> swordPool;
        
        [Space]
        [SerializeField] private Vector2 _throwDir;
        
        private void Awake()
        {
            swordPool = new ObjectPool<GameObject>(swordPrefab, 10);
        }
        
        protected override void Update()
        {
            base.Update();
        }

        public override bool CheckCanUse()
        {
            return base.CheckCanUse();
        }

        public override void UseSkill()
        {
            base.UseSkill();
            
            var sword = swordPool.Get();
            sword.transform.position = G.player.transform.position;
            sword.transform.rotation = G.player.transform.rotation;
            sword.GetComponent<SkillSwordThrowController>().Setup(_throwDir, 10);
        }

        private void OnGetObj(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReturnObj(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void OnDestroyObj(GameObject obj)
        {
            Destroy(obj);
        }
    }
}