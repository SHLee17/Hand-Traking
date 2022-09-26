using UnityEngine;


public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public Player player;
    [SerializeField]
    GameObject objOption;
    [SerializeField]
    GameObject objGM;

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
            player.cameraRig.centerEyeAnchor.transform.position.y + 0.5f,
            player.cameraRig.centerEyeAnchor.transform.position.z + 0.3f);
    }

    public void OnOption()
    {
        objOption.SetActive(!objOption.activeSelf);

        if(objGM == null)
            objGM = GameObject.FindGameObjectWithTag("GameManager");
        else
            objGM.SetActive(!objGM.activeSelf);
    }

    public T RandomEnum<T>(int min = 0)
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(new System.Random().Next(min, values.Length));
    }
}
