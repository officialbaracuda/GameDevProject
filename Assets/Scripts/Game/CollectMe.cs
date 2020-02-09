using UnityEngine;

public class CollectMe : MonoBehaviour
{
    private GameController gameController;
    private Animator animator;

    private bool active;
    [SerializeField] private int value;
    [SerializeField] private ItemType type;
    
    void Start()
    {
        active = true;
        gameController = GameController.Instance;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Player) {
            if (active) {
                gameController.Collect(this.type, value);
                animator.SetBool("isCollected", true);
                Destroy(this.gameObject, 1f);
                active = false;
            }
        }
    }
}
