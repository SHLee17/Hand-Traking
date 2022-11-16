using Oculus.Platform.Samples.VrHoops;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;


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
        public int total;
    }


    static GameManager instance;
    public Player player;
    [SerializeField]
    GameObject objOption;
    [SerializeField]
    GameObject objGM;

    [SerializeField]
    Transform parentSeletUserName;
    [SerializeField]
    List<SelectAlphabet> userName;

    public User currentUser;
    public bool isCameraSet;
    [SerializeField]
    float timer, restTimer;
    System.Random rand;
    string path;
    const int rankLenth = 10000;

    [SerializeField]
    TMP_Text txtRank;


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
        timer = restTimer = 3f;
        rand = new System.Random();
        if (!Directory.Exists($"{Application.dataPath}/User/"))
        {
            Directory.CreateDirectory($"{Application.dataPath}/User/");
            path = $"{Application.dataPath}/User/";
        }
        else
            path = $"{Application.dataPath}/User/";


        for (int i = 0; i < rankLenth; i++)
        {
            if (!PlayerPrefs.HasKey(i.ToString()))
                PlayerPrefs.SetInt(i.ToString(), 0);
        }

        PlayerPrefs.SetInt("UserCount", 0);

        if (ReferenceEquals(player, null))
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (objOption)
        {
            objOption.transform.position = new Vector3(
                transform.position.x,
                player.cameraRig.centerEyeAnchor.transform.position.y + 0.5f,
                player.cameraRig.centerEyeAnchor.transform.position.z + 0.3f);
        }

        if (objGM == null)
            objGM = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void Update()
    {
        if(objGM != null)
        {
            CameraUpdate(objGM.transform);
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
    List<int> rank = new List<int>();
    int currentPlayerRank;
    public void SetScoreBoard()
    {
        rank.Clear();

        int userCount = PlayerPrefs.GetInt("UserCount");
        PlayerPrefs.SetInt("UserCount", userCount++);

        for (int i = 0; i < rankLenth; i++)
        {
            rank.Add(PlayerPrefs.GetInt(i.ToString()));
        }

        for (int i = 0; i < rank.Count; i++)
        {
            if (rank[i] <= currentUser.total)
            {
                rank.Insert(i, currentUser.total);
                currentPlayerRank = i;
                break;
            }
        }

        txtRank.text = currentPlayerRank.ToString();

        for (int i = 0; i < rank.Count; i++)
            PlayerPrefs.SetInt(i.ToString(), rank[i]);

    }

    public void CameraUpdate(Transform transform)
    {
        if(Vector3.Distance(player.cameraRig.centerEyeAnchor.transform.position, Vector3.zero) != 0)
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            Vector3 pos = player.cameraRig.centerEyeAnchor.position;
            transform.position = new Vector3(pos.x, pos.y + 0.2f, pos.z + 0.3f);
        }
            
    }
    public void ResetTimer() => timer = restTimer;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 20), "CameraReset"))
        {
            if(objGM != null)
            CameraUpdate(objGM.transform);
        }
    }

}





