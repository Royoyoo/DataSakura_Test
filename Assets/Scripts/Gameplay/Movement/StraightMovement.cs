using Gameplay.Movement.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Movement
{
    public class StraightMovement : IMoveBehaviour
    {
        private readonly Rigidbody rigidbody;
        private readonly float speed;

        private Vector3 moveVector;


        public StraightMovement(StraightMoveBehaviourData straightMoveBehaviourData, Rigidbody animalRigidbody)
        {
            rigidbody = animalRigidbody;
            speed = straightMoveBehaviourData.Speed;

            Quaternion initialRotation = Quaternion.AngleAxis(Random.value * 360f, Vector3.up);
            moveVector = initialRotation * Vector3.forward * speed;
        }


        public void PhysicsUpdate(ZooBounds zooBounds)
        {
            BounceOffBounds(zooBounds);
            Move();
        }


        private void BounceOffBounds(ZooBounds zooBounds)
        {
            moveVector = zooBounds.BounceOffBounds(rigidbody, moveVector);
        }


        private void Move()
        {
            rigidbody.MoveRotation(Quaternion.LookRotation(moveVector, Vector3.up));
            rigidbody.linearVelocity = moveVector.normalized * speed;
        }
    }
}
