using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;
    public float frameDuration;

    private int currentSpriteId;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(UpdateSprite());
    }

    private IEnumerator UpdateSprite() {
        while (true) {
            yield return new WaitForSeconds(frameDuration);
            ChangeSprite();
        }
    }

    private void ChangeSprite() {
        if (sprites == null || sprites.Length == 0) {
            throw new System.Exception("No sprites have been assigned to the animator!");
        }

        spriteRenderer.sprite = sprites[currentSpriteId];
        
        currentSpriteId++;
        if (currentSpriteId == sprites.Length) {
            currentSpriteId = 0;
        }
    }
}
