using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // Diperlukan untuk me-restart game

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Character Data")]
    public CharacterData[] availableCharacters;

    [Header("UI References")]
    public GameObject characterSelectPanel;
    public Image p1_CharacterDisplay;
    public Image p2_CharacterDisplay;
    public TextMeshProUGUI countdownText;
    public Image player1HealthBarImage;
    public Image player2HealthBarImage;
    public TextMeshProUGUI p1_NameText;
    public TextMeshProUGUI p2_NameText;

    [Header("Game Setup")]
    public GameObject ballPrefab;
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;
    
    [Header("Effects")]
    public CameraShake cameraShakeController;
    public AudioClip attackSound;
    public AudioClip damageSound;
    public AudioClip wallBounceSound;
    public AudioClip scoreSound;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winLoseText;
    public AudioClip gameOverSound;
    
    private AudioSource audioSource;
    private int p1_selectedIndex = 0;
    private int p2_selectedIndex = 0;
    private Paddle player1Paddle;
    private Paddle player2Paddle;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 0;
        characterSelectPanel.SetActive(true);
        countdownText.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        UpdateCharacterDisplay();
    }
    
    public void PlayerScored(int scoringPlayerNumber)
    {
        if (cameraShakeController != null) cameraShakeController.TriggerShake();
        PlaySound(scoreSound);
        
        Paddle scoredOnPaddle = (scoringPlayerNumber == 1) ? player2Paddle : player1Paddle;
        Image healthBar = (scoringPlayerNumber == 1) ? player2HealthBarImage : player1HealthBarImage;

        if (scoredOnPaddle != null)
        {
            scoredOnPaddle.TakeDamage(1);
            float healthPercent = (float)scoredOnPaddle.currentHealth / (float)scoredOnPaddle.maxHealth;
            if (healthBar != null) healthBar.fillAmount = healthPercent;

            if (scoredOnPaddle.currentHealth <= 0)
            {
                string winner = (scoringPlayerNumber == 1) ? "Player 1" : "Player 2";
                HandleGameOver(winner);
                return;
            }
        }
        StartCoroutine(ServeBallAfterDelay(2f));
    }

    public void HandleGameOver(string winnerName)
    {
        Time.timeScale = 0f;
        winLoseText.text = winnerName + " Wins!";
        gameOverPanel.SetActive(true);
        PlaySound(gameOverSound);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    
    #region Unchanged Methods
    private void LaunchGame()
    {
        CharacterData p1_charData = availableCharacters[p1_selectedIndex];
        GameObject p1_gameObject = Instantiate(p1_charData.characterPrefab, player1SpawnPoint.position, Quaternion.identity);
        player1Paddle = p1_gameObject.GetComponent<Paddle>();
        player1Paddle.isPlayer1 = true;
        Vector3 p1_scale = p1_gameObject.transform.localScale;
        p1_scale.x *= -1;
        p1_gameObject.transform.localScale = p1_scale;

        CharacterData p2_charData = availableCharacters[p2_selectedIndex];
        GameObject p2_gameObject = Instantiate(p2_charData.characterPrefab, player2SpawnPoint.position, Quaternion.identity);
        player2Paddle = p2_gameObject.GetComponent<Paddle>();
        player2Paddle.isPlayer1 = false;
        
        if(player1HealthBarImage != null) player1HealthBarImage.fillAmount = 1f;
        if(player2HealthBarImage != null) player2HealthBarImage.fillAmount = 1f;
        ServeBall();
    }
    private void UpdateCharacterDisplay()
    {
        if (availableCharacters.Length == 0) return;
        p1_CharacterDisplay.sprite = availableCharacters[p1_selectedIndex].characterIcon;
        p2_CharacterDisplay.sprite = availableCharacters[p2_selectedIndex].characterIcon;
        p1_NameText.text = availableCharacters[p1_selectedIndex].characterName;
        p2_NameText.text = availableCharacters[p2_selectedIndex].characterName;
    }
    public void P1_NextCharacter()
    {
        p1_selectedIndex++;
        if (p1_selectedIndex >= availableCharacters.Length) p1_selectedIndex = 0;
        UpdateCharacterDisplay();
    }
    public void P1_PrevCharacter()
    {
        p1_selectedIndex--;
        if (p1_selectedIndex < 0) p1_selectedIndex = availableCharacters.Length - 1;
        UpdateCharacterDisplay();
    }
    public void P2_NextCharacter()
    {
        p2_selectedIndex++;
        if (p2_selectedIndex >= availableCharacters.Length) p2_selectedIndex = 0;
        UpdateCharacterDisplay();
    }
    public void P2_PrevCharacter()
    {
        p2_selectedIndex--;
        if (p2_selectedIndex < 0) p2_selectedIndex = availableCharacters.Length - 1;
        UpdateCharacterDisplay();
    }
    public void ConfirmSelections()
    {
        characterSelectPanel.SetActive(false);
        StartCoroutine(CountdownRoutine());
    }
    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
        LaunchGame();
    }
    public void ServeBall()
    {
        if (ballPrefab != null)
        {
            Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        }
    }
    private IEnumerator ServeBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ServeBall();
    }
    #endregion
}