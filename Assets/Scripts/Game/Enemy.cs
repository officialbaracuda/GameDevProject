using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform startPos, endPos;
    [SerializeField] private Vector3 currentPos;
    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioController audioController;
    [SerializeField] private PlayerMovement playerMovement;


    private int bossLifeCount = 5;

    private bool facingLeft;

    private void Start()
    {
        currentPos = startPos.position;
        InvokeRepeating("Routine", 0f, 2f);
    }
    public void Routine()
    {
        if (currentPos.x >= endPos.position.x)
        {
            StartCoroutine(MoveLeft());
        }

        if (currentPos.x <= startPos.position.x)
        {
            StartCoroutine(MoveRight());
        }
    }

    public void OnDead() {
        StopAllCoroutines();
        GetComponent<CapsuleCollider2D>().enabled = false;
        audioController.PlaySFX(audioController.kill);
        GetComponentInParent<Animator>().SetBool("isDead", true);
        if (this.gameObject.name.Contains("Fly") || this.gameObject.name.Contains("Bat")) {
            StartCoroutine(MoveDown());
        }
        GetComponentInChildren<CapsuleCollider2D>().enabled = false;
        Destroy(transform.parent.gameObject, 0.3f);
    }

    IEnumerator MoveRight()
    {
        while (currentPos.x <= endPos.position.x)
        {
            enemy.transform.position += new Vector3(0.10f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
            currentPos += new Vector3(0.10f, 0f, 0f);
        }
        Flip();
    }

    IEnumerator MoveLeft()
    {
        while (currentPos.x >= startPos.position.x)
        {
            enemy.transform.position -= new Vector3(0.10f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
            currentPos -= new Vector3(0.10f, 0f, 0f);
        }
        Flip();
    }

    IEnumerator MoveDown()
    {
        int count = 0;
        while (count < 5)
        {
            enemy.transform.position -= new Vector3(0f, 1f, 0f);
            yield return new WaitForSeconds(0.05f);
            count++;
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void HitBoss() {
        bossLifeCount--;
        playerMovement.Jump();
        StartCoroutine("HitBossAnim");
        if (bossLifeCount == 1) {
            this.transform.localScale = this.transform.localScale * 1.1f;
        }else if (bossLifeCount <= 0) {
            OnDead();
        }
    }

    IEnumerator HitBossAnim() {
        GetComponentInParent<Animator>().SetBool("hit", true);
        yield return new WaitForSeconds(0.1f);
        GetComponentInParent<Animator>().SetBool("hit", false);
    }

}