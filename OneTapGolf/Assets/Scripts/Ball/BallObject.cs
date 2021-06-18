using UnityEngine;

namespace OneTapGolf.Ball {
    public class BallObject : MonoBehaviour {
        private Ball ball;
        private void Start() {
            ball = new Ball(this, new Vector2(), new Vector2(0, -9.8f));
            ball.Throw(new Vector2(4f, 12));
        }

        private void Update() {
            ball.OnUpdate(Time.deltaTime);
        }
    }
}