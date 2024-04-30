using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{

    [SerializeField] private List<GameObject> skins;
    private int currentSkinIndex = 0;

    private void Start()
    {
        //if (skins.Count > 0)
        //{
        //    ShowCurrenSkin();
        //}
    }

    public void ChangeSkin(int index)
    {
        if (index >= 0 && index < skins.Count)
        {
            HideCurrentSkin();
            currentSkinIndex = index;
            ShowCurrenSkin();
        }
    }

    private void ShowCurrenSkin()
    {
        skins[currentSkinIndex].SetActive(true);
    }

    private void HideCurrentSkin()
    {
        skins[currentSkinIndex].SetActive(false);
    }
}
