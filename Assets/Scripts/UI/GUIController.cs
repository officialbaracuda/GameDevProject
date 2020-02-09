using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour
{
    [SerializeField] private Text coinCountText, nicknameText;
    [SerializeField] private InputField input;
    [SerializeField] private GameObject[] lives, stars;
    [SerializeField] private Sprite heart, emptyHeart;
    
    private string nickname;

    public void UpdateCoinCountText(int coins, int value) {
        StartCoroutine(UpdateHUDCoin(coins, value));
    }

    public void RemoveLive(int live) {
        lives[live].GetComponent<Animator>().Play("MakeEmpty");
    }

    public void AddLive(int live)
    {
        lives[live].GetComponent<Image>().sprite = heart;
    }

    IEnumerator UpdateHUDCoin(int coins, int value) {
        int total = coins + value;
        while (coins <= total) {
            coinCountText.text = "" + coins;
            yield return new WaitForSeconds(0.05f);
            coins++;
        }

        if(coins == total)
        {
            StopCoroutine("UpdateHUDCoin");
        }
    }

    public void AddStar(int star) {
        stars[star].GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void RemoveStars() {
        Debug.Log("Remove stars");
        for (int i = 0; i < stars.Length; i++) {
            stars[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }
    }

    public void SetNickname() {
        nickname = input.GetComponent<InputField>().text;
        nicknameText.text = "Welcome: " + nickname;
    }

    public void SaveNickname() {
        Debug.Log("Nickname: " + nickname);
        int nicknameCount = StorageController.GetInteger(Constants.Nickname + Constants.Count);
        Debug.Log("Count: " + nicknameCount);
        StorageController.SaveInteger(Constants.Nickname + Constants.Count, ++nicknameCount);
        StorageController.SaveString(Constants.Nickname + nicknameCount, nickname);
        StorageController.SaveString(Constants.Nickname + Constants.Current, nickname);
    }
}
