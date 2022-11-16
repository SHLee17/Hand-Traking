using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class User
    {
        public string name;
        public int num;
        public int memory;
        public int card;
        public int match;
        public int rsp;
        public int serialnumber;
    }

    static GameManager instance;
    public Player player;
    [SerializeField]
    GameObject objOption;
    [SerializeField]
    GameObject objGM;

    public List<GameObject> terrains;

    [SerializeField]
    Transform parentSeletUserName;
    [SerializeField]
    List<SelectAlphabet> userName;

    public User currentUser;
    System.Random rand;
    string path;

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
        rand = new System.Random();
        if (!Directory.Exists($"{Application.dataPath}/User/"))
        {
            Directory.CreateDirectory($"{Application.dataPath}/User/");
            path = $"{Application.dataPath}/User/";
        }
        else
            path = $"{Application.dataPath}/User/";

        //CreateUser();
        //SaveUser();

        if (ReferenceEquals(player, null))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (objOption)
        {
            objOption.transform.position = new Vector3(
                transform.position.x,
                player.cameraRig.centerEyeAnchor.transform.position.y + 0.5f,
                player.cameraRig.centerEyeAnchor.transform.position.z + 0.3f);
        }
    }

    public void OnOption()
    {
        objOption.SetActive(!objOption.activeSelf);

        if (objGM == null)
            objGM = GameObject.FindGameObjectWithTag("GameManager");

        if (objGM != null)
            objGM.SetActive(!objGM.activeSelf);
    }

    public T RandomEnum<T>(int min = 0)
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(new System.Random().Next(min, values.Length));
    }
    public void SaveUser()
    {

        string json = JsonUtility.ToJson(currentUser);

        Debug.Log(json);

        if (!File.Exists($"{path}{currentUser.name}{currentUser.serialnumber}.json"))
            File.WriteAllText($"{path}{currentUser.name}{currentUser.serialnumber}.json", json);
        else
        {
            currentUser.serialnumber = rand.Next(100000);
            File.WriteAllText($"{path}{currentUser.name}{currentUser.serialnumber}.json", json);
        }

        AssetDatabase.Refresh();
    }

    public void CreateUser()
    {
        currentUser = new User();

        parentSeletUserName = GameObject.FindGameObjectWithTag("NameSelect").transform;

        if (parentSeletUserName != null)
        {
            foreach (Transform item in parentSeletUserName)
                currentUser.name += item.GetComponent<SelectAlphabet>().alphabet;
        }
    }

    public void ChangeTerrain(int num)
    {
        foreach (var item in terrains)
            item.SetActive(false);

        terrains[num].SetActive(true);
    }
}
