using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Transform startPos, endPos;
    [SerializeField] private Vector3 currentPos;
    [SerializeField] private Sprite switchOn;
    [SerializeField] private GameObject bridge;
    [SerializeField] private AudioController audioController;

    private void Start()
    {
        isActive = false;
        currentPos = startPos.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Player && !isActive) {
            isActive = true;
            audioController.PlaySFX(audioController.activate);
            GetComponent<SpriteRenderer>().sprite = switchOn;
            InvokeRepeating("BridgeRoutine", 0.5f, 2f);
        }
    }

    public void BridgeRoutine() {

        if (currentPos.x >= endPos.position.x)
        {
            StartCoroutine(MoveLeft());
        }

        if (currentPos.x <= startPos.position.x)
        {
            StartCoroutine(MoveRight());
        }

    }

    IEnumerator MoveRight()
    {
        while (currentPos.x <= endPos.position.x)
        {
            bridge.transform.position += new Vector3(0.10f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
            currentPos += new Vector3(0.10f, 0f, 0f);
        }
    }

    IEnumerator MoveLeft()
    {
        while (currentPos.x >= startPos.position.x)
        {
            bridge.transform.position -= new Vector3(0.10f, 0f, 0f);
            yield return new WaitForSeconds(0.05f);
            currentPos -= new Vector3(0.10f, 0f, 0f);
        }
    }

}
