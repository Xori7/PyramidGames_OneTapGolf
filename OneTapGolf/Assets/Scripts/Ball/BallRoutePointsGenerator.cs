using System.Collections.Generic;
using UnityEngine;

namespace OneTapGolf.Ball {
    public class BallRoutePointsGenerator {
        private readonly Ball ball;

        public BallRoutePointsGenerator(Ball ball) {
            this.ball = ball;
        }

        public Vector2[] GetPoints(float timeBetweenPoints) {
            List<Vector2> result = new List<Vector2>();
            while (ball.physicalObject.position.y >= ball.groundLevel) {
                ball.OnUpdate(0.1f);
                result.Add(ball.physicalObject.position);
            }

            return result.ToArray();
        }
    }
}