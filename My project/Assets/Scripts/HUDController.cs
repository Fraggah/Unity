using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{

    [SerializeField] GameObject lifeIcon;
    [SerializeField] GameObject lifeIconsContainer;

    public void UpdateLifesHUD(int lifes)
    {

        if (IsEmptyContainer())
        {
            LoadContainer(lifes);
            return;
        }

        if (HowManyLifeIcons() > lifes)
        {
            DeleteLastIcon();
        }
        else
        {
            CreateIcon();
        }

    }

    private bool IsEmptyContainer()
    {
        return lifeIconsContainer.transform.childCount == 0;
    }

    private int HowManyLifeIcons()
    {
        return lifeIconsContainer.transform.childCount;
    }

    private void DeleteLastIcon()
    {
        Transform container = lifeIconsContainer.transform;
        GameObject.Destroy(container.GetChild(container.childCount - 1).gameObject);
    }

    private void LoadContainer(int countIcons)
    {
        for (int i = 0; i < countIcons; i++)
        {
            CreateIcon();
        }
    }

    private void CreateIcon()
    {
        Instantiate(lifeIcon, lifeIconsContainer.transform);
    }
}
