using UnityEngine;

public class Goal : MonoBehaviour
{
    public int scoringPlayerNumber;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerScored(scoringPlayerNumber);
            }
            Destroy(other.gameObject);
        }
    }
}