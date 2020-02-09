using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    [SerializeField] PlayerMovement movement;
    private Vector3 initialPosition;

    void Start()
    {
        gameController = GameController.Instance;
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Killable)
        {
            if (collision.transform.parent.gameObject.tag != Tags.Boss)
            {
                collision.gameObject.GetComponentInParent<Enemy>().OnDead();
            }
            else {
                collision.gameObject.GetComponentInParent<Enemy>().HitBoss();
                movement.Jump();
            }
        }

        if (collision.tag == Tags.SavePoint) {
            collision.gameObject.GetComponent<Animator>().SetBool("isSaved", true);
            initialPosition = transform.position;
        }

        if (collision.tag == Tags.Exit)
        {
            gameController.NextLevel();
        }

        if (collision.tag == Tags.Finish)
        {
            if (GameObject.FindGameObjectWithTag(Tags.Boss) == null) {
                gameController.FinishGame();
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Enemy || collision.gameObject.tag == Tags.Boss)
        {
            gameController.PlayerDie();
            transform.position = initialPosition;
        }
    }
}
