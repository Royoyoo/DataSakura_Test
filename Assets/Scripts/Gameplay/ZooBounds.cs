using Core.Configs;
using UnityEngine;

namespace Gameplay
{
    public class ZooBounds
    {
        private readonly Vector2 bounds;


        public ZooBounds(MainConfig mainConfig)
        {
            bounds = new Vector2(mainConfig.ZooSize, mainConfig.ZooSize / 2f);
        }


        public Vector3 GetRandomPointInBounds()
        {
            return new Vector3(Random.Range(-bounds.x, bounds.x) / 2f, 0f, Random.Range(-bounds.y, bounds.y) / 2f);
        }


        public Vector3 BounceOffBounds(Rigidbody rigidbody, Vector3 moveVector)
        {
            if (Mathf.Abs(rigidbody.position.x) > bounds.x / 2f)
            {
                float sign = Mathf.Sign(rigidbody.position.x);
                Vector3 inNormal = Vector3.right * -sign;
                moveVector = Vector3.Reflect(rigidbody.linearVelocity, inNormal);
                rigidbody.Move(new Vector3(sign * (bounds.x / 2f), rigidbody.position.y, rigidbody.position.z), Quaternion.identity);
            }

            if (Mathf.Abs(rigidbody.position.z) > bounds.y / 2f)
            {
                float sign = Mathf.Sign(rigidbody.position.z);
                Vector3 inNormal = Vector3.forward * -sign;
                moveVector = Vector3.Reflect(rigidbody.linearVelocity, inNormal);
                rigidbody.Move(new Vector3(rigidbody.position.x, rigidbody.position.y, sign * (bounds.y / 2f)), Quaternion.identity);
            }

            return moveVector;
        }
    }
}
