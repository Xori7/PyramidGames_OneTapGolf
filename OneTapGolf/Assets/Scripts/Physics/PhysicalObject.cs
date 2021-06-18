using UnityEngine;

namespace OneTapGolf.Physics {
    public class PhysicalObject {
        public Vector2 position;
        public Vector2 velocity;
        internal Vector2 acceleration;

        public PhysicalObject(Vector2 position) {
            this.position = position;
        }

        ///<summary>
        ///Changes the force working on the object by force argument
        ///</summary>
        public void AddAcceleration(Vector2 force) {
            this.acceleration += force;
        }
    }
}
