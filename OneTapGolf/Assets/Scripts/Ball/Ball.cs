using OneTapGolf.Physics;
using UnityEngine;

namespace OneTapGolf.Ball {
    public class Ball {
        public readonly PhysicalObject physicalObject;
        public readonly float groundLevel;
        private readonly IPhysicalObjectVelocityUpdater velocityUpdater;
        private readonly IPhysicalObjectPositionUpdater positionUpdater;
        private readonly float groundFrictionAcceleration;
        private readonly float bounceAccelerationLoss;

        public Ball(Vector2 position, Vector2 gravityAcceleration, 
            float groundLevel, float groundFrictionAcceleration, float bounceAccelerationLoss) {
            physicalObject = new PhysicalObject(position);
            physicalObject.AddAcceleration(gravityAcceleration);
            velocityUpdater = new DefaultPhysicalObjectVelocityUpdater(physicalObject);
            positionUpdater = new DefaultPhysicalObjectPositionUpdater(physicalObject);
            this.groundLevel = groundLevel;
            this.groundFrictionAcceleration = groundFrictionAcceleration;
            this.bounceAccelerationLoss = bounceAccelerationLoss;
        }

        public void Throw(Vector2 startVelocity) {
            physicalObject.velocity += startVelocity;
        }

        public void OnUpdate(float timeElapsed) {
            if (physicalObject.position.y < groundLevel) {
                OnCollisionWithGround();
                //Calculate ground friction
                physicalObject.velocity += new Vector2(Mathf.Clamp(groundFrictionAcceleration, -physicalObject.velocity.x, float.MaxValue), 0);
            }

            velocityUpdater.UpdatePhysicalObjectVelocity(timeElapsed);
            positionUpdater.UpdatePhysicalObjectPosition(timeElapsed);
        }

        private void OnCollisionWithGround() {
            //Set y position to ground position when ball tries to escape the map
            physicalObject.position = new Vector2(physicalObject.position.x, groundLevel);

            //Add velocity in opposite direction as in 3 Newton's law of motion
            Throw(new Vector2(0, -physicalObject.velocity.y * (2 - bounceAccelerationLoss)));
        }
    }
}