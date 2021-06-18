using OneTapGolf.Physics;
using UnityEngine;

namespace OneTapGolf.Ball {
    public class Ball {
        private readonly PhysicalObject physicalObject;
        private readonly BallObject ballObject;
        private readonly IPhysicalObjectVelocityUpdater velocityUpdater;
        private readonly IPhysicalObjectPositionUpdater positionUpdater;

        public Ball(BallObject ballObject, Vector2 position, Vector2 gravityForce) {
            this.ballObject = ballObject;
            physicalObject = new PhysicalObject(position);
            physicalObject.AddForce(gravityForce);
            velocityUpdater = new DefaultPhysicalObjectVelocityUpdater(physicalObject);
            positionUpdater = new DefaultPhysicalObjectPositionUpdater(physicalObject);
        }

        public void Throw(Vector2 startVelocity) {
            physicalObject.velocity += startVelocity;
        }

        public void OnUpdate(float timeElapsed) {
            velocityUpdater.UpdatePhysicalObjectVelocity(timeElapsed);
            positionUpdater.UpdatePhysicalObjectPosition(timeElapsed);
            ballObject.transform.position = physicalObject.position;
        }
    }
}