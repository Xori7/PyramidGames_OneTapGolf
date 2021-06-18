using UnityEngine;

namespace OneTapGolf.Physics {
    public class PhysicalObject {
        public Vector2 position;
        public Vector2 velocity;
        internal Vector2 force;

        public PhysicalObject(Vector2 position) {
            this.position = position;
        }

        ///<summary>
        ///Changes the force working on the object by force argument
        ///</summary>
        public void AddForce(Vector2 force) {
            this.force += force;
        }
    }
}
