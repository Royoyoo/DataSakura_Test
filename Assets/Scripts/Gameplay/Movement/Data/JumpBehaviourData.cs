using UnityEngine;
using VContainer;

namespace Gameplay.Movement.Data
{
    public class JumpBehaviourData : IMoveBehaviourData
    {
        public float DesiredJumpDistance = 5f;
        public float DesiredJumpHeight = 2f;
        public float JumpDelay = 2f;


        public IMoveBehaviour CreateMoveBehaviour(Rigidbody rigidbody) => new JumpMovement(this, rigidbody);
    }
}
