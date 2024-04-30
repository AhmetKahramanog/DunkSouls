using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShardsCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collected;

    [SerializeField] private TextMeshProUGUI woolCollected;

    public static ShardsCount Instance = new();

    private int shardsCart;

    public int woolCart { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //collected.text = shardsCart.ToString();
    }

    private void Update()
    {
        collected.text = shardsCart.ToString();
        woolCollected.text = woolCart.ToString();
    }

    public int ShardsCart()
    {
        shardsCart += 20;
        return shardsCart;
        //PlayerPrefs.SetInt("ShardCount", shardsCart);
    }

    public int WoolCart()
    {
        woolCart += 5;
        return woolCart;
        //PlayerPrefs.SetInt("WoolCount", woolCart);
    }

}
