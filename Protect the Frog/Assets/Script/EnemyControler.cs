using System.Collections;
using UnityEngine;
public class EnemyControler : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _enemyHP;
    private GameObject _player;
    private bool _canDamage = true;
    private PlayerControler _playerController;
    
    private void Start()
    {
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerControler>();
        transform.Rotate(0, transform.position.x > 0 ? 180 : 0, 0);
    }
    private void Update()
    {
        MoveToTarget();
    }
      
    private void MoveToTarget()
    {
        float targetX = _player.transform.position.x;
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.MoveTowards(transform.position.x, targetX, _speed * Time.deltaTime);
        transform.position = newPosition;
    }
 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn" && _canDamage)
        {
            StartCoroutine(CanDamageTime());
            _enemyHP -= 1;
            CheckIfAlive();
        }
    }
    private IEnumerator CanDamageTime()
    {
        _canDamage = false;
        yield return new WaitForSeconds(0.4f);
        _canDamage = true;
    }

    private void CheckIfAlive()
    {
        if (_enemyHP <= 0)
        {
            _playerController.ChangeDefeatedEnemyCount();
            Destroy(gameObject);
        }
    }
}
