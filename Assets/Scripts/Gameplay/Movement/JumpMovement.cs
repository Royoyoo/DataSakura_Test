using Gameplay.Movement.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Movement
{
    public class JumpMovement : IMoveBehaviour
    {
        private readonly Rigidbody rigidbody;
        private readonly float jumpDelay;

        private float jumpTimer;
        private float horizontalJumpForce;
        private float verticalJumpForce;


        public JumpMovement(JumpBehaviourData jumpBehaviourData, Rigidbody animalRigidbody)
        {
            jumpDelay = jumpBehaviourData.JumpDelay;

            (horizontalJumpForce, verticalJumpForce) =
                PreCalculateVelocity(jumpBehaviourData.DesiredJumpDistance, jumpBehaviourData.DesiredJumpHeight);

            rigidbody = animalRigidbody;

            jumpTimer = Random.value * jumpDelay;
        }


        public void PhysicsUpdate(ZooBounds zooBounds)
        {
            BounceOffBounds(zooBounds);

            jumpTimer += Time.fixedDeltaTime;

            if (jumpTimer < jumpDelay)
            {
                return;
            }

            Jump();
            jumpTimer -= jumpDelay;
        }


        private void BounceOffBounds(ZooBounds zooBounds)
        {
            rigidbody.linearVelocity = zooBounds.BounceOffBounds(rigidbody, rigidbody.linearVelocity);
        }


        private void Jump()
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            Vector3 jumpDirection = new(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
            Vector3 jumpForce = GetJumpForce(rigidbody.mass, jumpDirection);

            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }


        private Vector3 GetJumpForce(float mass, Vector3 horizontalDirection)
        {
            return horizontalDirection * (mass * horizontalJumpForce) + Vector3.up * verticalJumpForce;
        }


        private static (float, float) PreCalculateVelocity(float horizontalDistance, float maxHeight)
        {
            float gravity = Mathf.Abs(Physics.gravity.y);
            float verticalForce = Mathf.Sqrt(2f * gravity * maxHeight);
            float timeOfFlight = 2f * verticalForce / gravity;
            float horizontalForce = horizontalDistance / timeOfFlight;

            return (horizontalForce, verticalForce);
        }
    }
}
