using System.Collections.Generic;
using UnityEngine;

namespace OneTapGolf.Ball {
    public class BallRoutePointsGenerator {
        private readonly BallController ball;

        public BallRoutePointsGenerator(BallController ball) {
            this.ball = ball;
        }

        ///<summary>
        ///Predicts ball route in specified physical environment
        ///</summary>
        ///<returns>
        ///Positions that are placed along predicted ball route in specified physical environment.
        ///Each position is equal to predicted ball position in every multiple of "timeBetweenPoints"
        ///</returns>
        ///<param name="timeBetweenPoints">Time in seconds, which will past between every returning position based on predicted ball movement</param>
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