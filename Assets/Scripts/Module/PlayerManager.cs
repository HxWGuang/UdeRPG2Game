using UnityEngine;

namespace Hx.Module
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        [HideInInspector] public Player player;

        private void Awake()
        {
            if (instance != null) Destroy(gameObject);
            else instance = this;
        }
    }
}