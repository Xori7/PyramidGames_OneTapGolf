using UnityEngine;

namespace OneTapGolf.Physics {
    public interface IPhysicalObjectVelocityUpdater {
        void UpdatePhysicalObjectVelocity(float timeElapsed, Vector2 additionalForce = default);
    }
}