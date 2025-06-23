using UnityEngine;

namespace Gameplay.Movement.Data
{
    public class StraightMoveBehaviourData : IMoveBehaviourData
    {
        public float Speed = 5f;


        public IMoveBehaviour CreateMoveBehaviour(Rigidbody rigidbody) => new StraightMovement(this, rigidbody);
    }
}
