using UnityEngine;

namespace OneTapGolf.Physics {
    public sealed class DefaultPhysicalObjectVelocityUpdater : IPhysicalObjectVelocityUpdater {
        private readonly PhysicalObject physicalObject;

        public DefaultPhysicalObjectVelocityUpdater(PhysicalObject physicalObject) {
            this.physicalObject = physicalObject ?? throw new System.ArgumentNullException(nameof(physicalObject));
        }

        public void UpdatePhysicalObjectVelocity(float timeElapsed, Vector2 additionalAcceleration = default) {
            physicalObject.velocity += (physicalObject.acceleration + additionalAcceleration) * timeElapsed;
        }
    }
}