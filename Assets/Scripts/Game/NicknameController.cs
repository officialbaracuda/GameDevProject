using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameController : MonoBehaviour
{
    public static NicknameController Instance { get; private set; }

    [SerializeField] private GameObject enterName;
    private bool isGame;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isGame = false;
        SetEnterName();
    }

    public void SetIsGame() {
        isGame = true;
    }

    public void SetEnterName()
    {
        if (isGame)
        {
            enterName.SetActive(false);
        }
        else
        {
            enterName.SetActive(true);
        }
    }
}
