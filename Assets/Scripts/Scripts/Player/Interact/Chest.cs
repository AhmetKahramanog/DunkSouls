using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactText;

    private bool isChestOpen = false;

    [SerializeField] private Transform chestHead;
    [SerializeField] private float chestOpenSpeed;
    [SerializeField] private GameObject ironShardsInfo;

    [SerializeField] private List<Chest> chests;

    [SerializeField] private GameObject woolInfo;

    private bool isChestRange = false;

    private int collectCount = 0;


    private void Awake()
    {
        ironShardsInfo.SetActive(false);
        woolInfo.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isChestRange)
        {
            isChestOpen = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isChestRange = true;
            interactText.gameObject.SetActive(true);
            if (isChestOpen)
            {
                ChestOpen();
                interactText.gameObject.SetActive(false);
                collectCount++;
                if (collectCount == 1)
                {
                    ironShardsInfo.SetActive(true);
                    StartCoroutine(DelayInfo(2f));
                    ShardsCount.Instance.ShardsCart();
                    foreach (var woolChest in chests)
                    {
                        if (woolChest)
                        {
                            woolInfo.SetActive(true);
                            ShardsCount.Instance.WoolCart();
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            interactText.gameObject.SetActive(false);
            isChestRange = false;
        }
    }

    private void ChestOpen()
    {
        if (chestHead.transform.rotation.x <= 0.5f)
        {
            var quarnation = Quaternion.Euler(chestHead.transform.eulerAngles.x + (-chestOpenSpeed * Time.deltaTime), -90f, 0f);
            chestHead.transform.rotation = quarnation;
        }
    }

    private IEnumerator DelayInfo(float time)
    {
        yield return new WaitForSeconds(time);
        ironShardsInfo.SetActive(false);
        woolInfo.SetActive(false);
    }
}
