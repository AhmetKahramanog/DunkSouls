using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInteract : MonoBehaviour
{
    private bool isShopRange = false;
    public static bool IsShopOpen { get; set; } = false;

    [SerializeField] private GameObject interact;

    [SerializeField] private GameObject showShopUI;

    [SerializeField] private GameObject showWeaponUI;

    [SerializeField] private GameObject showClothUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isShopRange)
        {
            IsShopOpen = !IsShopOpen;
        }
        if (!isShopRange)
        {
            IsShopOpen = false;
        }

        if (!IsShopOpen)
        {
            ResetShopIU();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isShopRange = true;
        }
        if (IsShopOpen)
        {
            interact.SetActive(false);
            showShopUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            interact.SetActive(true);
            showShopUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isShopRange = false;
            interact.SetActive(false);
        }
    }

    public void WeaponButton()
    {
        showWeaponUI.SetActive(true);
        showClothUI.SetActive(false);
    }

    public void ClothButton()
    {
        showWeaponUI.SetActive(false);
        showClothUI.SetActive(true);
    }

    private void ResetShopIU()
    {
        showClothUI.SetActive(false);
        showWeaponUI.SetActive(false);
    }
}
