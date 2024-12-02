using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<CheckpointManager>().SetCheckpoint(transform.position);
            Color color;
            ColorUtility.TryParseHtmlString("#00db4d", out color);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }
    }
}

