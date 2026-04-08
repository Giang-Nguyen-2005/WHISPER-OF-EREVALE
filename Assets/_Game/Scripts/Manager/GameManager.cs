using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;

    private bool isGameOver= false;
    private void Awake()
    {
        if(Instance ==null) Instance =this;
        else Destroy(gameObject);

        Time.timeScale =1f;
        if(gameOverPanel !=null) gameOverPanel.SetActive(false);
    }

    public void EndGame()
    {
        if(isGameOver) return;

        isGameOver =true;
        Debug.Log("Game Over");

        Time.timeScale =0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
