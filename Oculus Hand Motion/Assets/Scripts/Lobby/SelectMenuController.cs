using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI header;
    public TMPro.TextMeshProUGUI body;
    public ImageController[] screenShots;

    public void setThisMenu(LobbyManager.MenuSheet thisMenu)
    {
        header.text = thisMenu.title;
        body.text = thisMenu.description;
        for (int i = 0; i < thisMenu.sprites.Count; i++)
        {
            screenShots[i].SetImage(thisMenu.sprites[i]);
        }
    }
}

