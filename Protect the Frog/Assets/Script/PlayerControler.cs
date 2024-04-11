using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [HideInInspector] public int EnemiesDefeatedCurrent;
    private int _playerHp = 1;
    [SerializeField] private TextMeshProUGUI textOfDefeatedEnemy;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnClickLeftArrow();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnClickRightArrow();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Minus main character hit points
        _playerHp--;
        if (_playerHp <= 0)
        {
            _losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Destroy(other.gameObject);
        }

    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    private void OnClickLeftArrow()
    {
        _animator.Play("leftAtack");
    }
    
    private void OnClickRightArrow()
    {
        _animator.Play("rightAtack");
    }

    public void ChangeDefeatedEnemyCount()
    {
        EnemiesDefeatedCurrent++;
        textOfDefeatedEnemy.text = EnemiesDefeatedCurrent + "";
    }
}
