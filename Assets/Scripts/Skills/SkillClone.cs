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
            clone.GetComponent<SkillCloneController>().Setup();
        }

        private void OnObjGet(GameObject obj)
        {
            // Note: 只能直接设置GameObject的SetActive，不能直接设置Animator的enabled
            // 设置Animator的enabled会保留Animator的状态，导致动画状态混乱
            // 相关：keepAnimatorStateOnDisable，但是这个属性只能控制GameObject的Active
            
            obj.SetActive(true);

            // obj.GetComponent<Animator>().enabled = true;
            // obj.GetComponent<Renderer>().enabled = true;
            // obj.GetComponent<SkillCloneController>().enabled = true;
        }
        private void OnObjReturn(GameObject obj)
        {
            obj.SetActive(false);

            // obj.GetComponent<Animator>().enabled = false;
            // obj.GetComponent<Renderer>().enabled = false;
            // obj.GetComponent<SkillCloneController>().enabled = false;
        }
        private void OnObjDestroy(GameObject obj)
        {
            
            Destroy(obj);
        }
    }
}