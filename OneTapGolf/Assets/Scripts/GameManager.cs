using UnityEngine;
using OneTapGolf.Ball;
using UnityEngine.UI;

namespace OneTapGolf {
    public class GameManager : MonoBehaviour {
        public static GameManager Singleton { get; private set; }
        public Target target;
        public BallObject ball;

        public Text scoreText;
        public Text lostText;

        public float xVelocity, yVelocity;
        public float CameraHorizontalRange { get; private set; }

        private int level;
        private int best;


        private void Awake() {
            Singleton = this;
            level = -1;
            LoadNextLevel();
        }

        public void LoadNextLevel() {
            CameraHorizontalRange = Camera.main.orthographicSize * Screen.width / Screen.height;
            target.transform.position = new Vector3(Random.Range(-CameraHorizontalRange + 3.0f, CameraHorizontalRange - 1.0f), target.transform.position.y, 0.0f);
            ball.transform.position = new Vector3(-CameraHorizontalRange + 1.0f, -1.68f, 0.0f);
            ball.xVelocity *= 1.5f;
            ball.yVelocity *= 1.5f;
            ball.Initialize();
            level++;
            scoreText.text = $"SCORE: {level}";
        }

        public void Lost() {
            lostText.text = $"GAME OVER!\n\nSCORE: {level} BEST: {best}";
            lostText.gameObject.SetActive(true);
            if (level > best) {
                best = level;
            }
        }

        public void StartAgain() {
            level = -1;
            LoadNextLevel();
            ball.xVelocity = xVelocity;
            ball.yVelocity = yVelocity;
            lostText.gameObject.SetActive(false);
            scoreText.text = $"SCORE: 0";
        }
    }
}