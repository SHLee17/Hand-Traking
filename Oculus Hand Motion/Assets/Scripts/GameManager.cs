using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction.Input;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public Player player;
    [SerializeField]
    GameObject objOption;

    public static GameManager Instance 
    {
        get
        {
            if (ReferenceEquals(instance, null))
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (ReferenceEquals(instance, null))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (ReferenceEquals(player, null))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        objOption.transform.position = new Vector3(
            transform.position.x, 
            player.cameraRig.centerEyeAnchor.transform.position.y + 1,
            player.cameraRig.centerEyeAnchor.transform.position.z + 0.3f);
    }

    public void OnOption()
    {
        objOption.SetActive(!objOption.activeSelf);
    }
}
