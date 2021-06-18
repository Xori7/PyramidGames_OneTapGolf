using UnityEngine;

namespace OneTapGolf {
    public class Target : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject == GameManager.Singleton.ball.gameObject) {
                GameManager.Singleton.LoadNextLevel();
            }
        }
    }
}