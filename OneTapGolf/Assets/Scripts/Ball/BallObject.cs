using UnityEngine;

namespace OneTapGolf.Ball {
    public class BallObject : MonoBehaviour {
        private Ball ball;

        private void Start() {
            ball = new Ball(this, new Vector2(-9, 0f), new Vector2(0, -9.8f), -1.68f, -0.01f, 0.4f);
            ball.Throw(new Vector2(1f, 12));
        }

        private void FixedUpdate() {
            ball.OnUpdate(Time.fixedDeltaTime);
        }
    }
}