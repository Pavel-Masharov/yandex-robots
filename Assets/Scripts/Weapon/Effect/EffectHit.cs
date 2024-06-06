using UnityEngine;

namespace Services.Weapons
{
    public abstract class EffectHit : MonoBehaviour
    {
        protected abstract float Damage { get; }
        public abstract void ActionEffect();
    }
}
 