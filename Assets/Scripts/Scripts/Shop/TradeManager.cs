using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    private ChangeClothes changeCloth;

    [SerializeField] private List<GameObject> buyButtons;

    [SerializeField] private List<GameObject> claimedButtons;

    private int currentButton = 0;

    private void Start()
    {
        changeCloth = FindAnyObjectByType<ChangeClothes>();
    }

    private void Update()
    {

    }

    public void BuyPijama()
    {
        if (ShardsCount.Instance.woolCart >= 5)
        {
            currentButton = 0;
            ShardsCount.Instance.woolCart -= 5;
            changeCloth.ChangeSkin(0);
            SuccessBuy();
        }
    }

    public void BuySchoolUniform()
    {
        if (ShardsCount.Instance.woolCart >= 10)
        {
            currentButton = 1;
            ShardsCount.Instance.woolCart -= 10;
            changeCloth.ChangeSkin(1);
            SuccessBuy();
        }
    }

    private void SuccessBuy()
    {
        //for (int i = 0; i < buyButtons.Count; i++)
        //{
        //    for(int j = 0; j < claimedButtons.Count; j++)
        //    {
        //        buyButtons[currentButton].SetActive(false);
        //        buyButtons[currentButton].SetActive(true);
        //    }
        //}
        buyButtons[currentButton].SetActive(false);
        claimedButtons[currentButton].SetActive(true);
    }
}
