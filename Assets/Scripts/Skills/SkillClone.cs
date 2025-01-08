using Hx.Module;
using Hx.Utils;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillClone : SkillBase
    {
        [SerializeField] private GameObject _clonePrefab;
        
        public ObjectPool<GameObject> objectPool;

        private void Awake()
        {
            objectPool = new ObjectPool<GameObject>(_clonePrefab, 10, OnObjGet, OnObjReturn, OnObjDestroy);
        }

        public override void UseSkill()
        {
            base.UseSkill();
            var clone = objectPool.Get();
            clone.transform.position = G.player.transform.position;
            clone.transform.rotation = G.player.transform.rotation;
        }

        private void OnObjGet(GameObject obj)
        {
            obj.GetComponent<Renderer>().enabled = true;
            obj.GetComponent<SkillCloneController>().enabled = true;
        }
        private void OnObjReturn(GameObject obj)
        {
            obj.GetComponent<Renderer>().enabled = false;
            obj.GetComponent<SkillCloneController>().enabled = false;
        }
        private void OnObjDestroy(GameObject obj)
        {
            
            Destroy(obj);
        }
    }
}