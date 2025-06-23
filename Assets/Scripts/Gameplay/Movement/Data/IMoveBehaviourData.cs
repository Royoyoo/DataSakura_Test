using UnityEngine;

namespace Gameplay.Movement.Data
{
    public interface IMoveBehaviourData
    {
        public IMoveBehaviour CreateMoveBehaviour(Rigidbody rigidbody);
    }
}
