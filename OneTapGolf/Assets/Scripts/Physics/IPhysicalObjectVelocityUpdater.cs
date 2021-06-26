using UnityEngine;

namespace OneTapGolf.Physics {
    public interface IPhysicalObjectVelocityUpdater {
        ///<summary>
        ///Sets physical object velocity to new one that object has after time "timeElapsed", base on implemented physic system
        ///</summary>
        ///<param name="additionalAcceleration">Force that is added to the object acceleration for that one calculation and has impact on final object velocity</param>
        void UpdatePhysicalObjectVelocity(float timeElapsed, Vector2 additionalAcceleration = default);
    }
}