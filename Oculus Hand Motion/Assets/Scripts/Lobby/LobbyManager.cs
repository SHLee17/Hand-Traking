using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public SelectMenuController[] menus;
    public const int titleNum = 5;
    private string[] titles;
    private string[] descriptions;
    private int[] imageNum;
    public Sprite[] images;
    public int selectedMenu;
    private int selectTemp;

    private void Awake()
    {
        MenuContents();
        selectedMenu = 0;
        selectTemp = selectedMenu;
        SetMenus();
    }

    private void Update()
    {
        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y - .2f, transform.position.z);
        //transform.LookAt(GameManager.Instance.player.cameraRig.transform);

        if (selectedMenu == selectTemp)
            return;

        SetMenus();
    }

    public void SetMenus()
    {
        if (selectedMenu < 0)
            selectedMenu += titleNum;
        if (selectedMenu >= titleNum)
            selectedMenu -= titleNum;

        for (int i = 0; i < 3; i++)
        {
            int menuNum = selectedMenu + i >= titleNum ? selectedMenu + i - titleNum : selectedMenu + i;
            List<Sprite> sprTemp = new List<Sprite>();
            for (int j = imageNum[menuNum]; j < (menuNum + 1 >= titleNum ? images.Length : imageNum[menuNum + 1]); j++) 
            {
                sprTemp.Add(images[j]);
            }
            MenuSheet temp = new MenuSheet(titles[menuNum], descriptions[menuNum], sprTemp);
            menus[i].setThisMenu(temp);
        }
        selectTemp = selectedMenu;
    }

    public void MenuContents()
    {
        titles = new string[titleNum] {
            "<rainb f=0.2>������ ��Ģ���� ����</rainb>",
            "<rainb f=0.2>���� �ű�� ����</rainb>",
            "<rainb f=0.2>ī�� ������ ����</rainb>",
            "<rainb f=0.2>���� ���� ��</rainb>",
            "<rainb f=0.2>���� �׽�Ʈ</rainb>"
        };
        descriptions = new string[titleNum] {
            "ȭ�鿡�� ��ģ �հ��� ������ŭ ���ڰ� ǥ�õſ�.\nȭ�鿡 ���� ���� ���� ���� ������ ������ ����\n�հ����� ���ļ� �����ּ���.\n",
            "���ɰ���� ����̳� ���ڸ� ��������.\n���ɰ��� �Űܼ� ������ ���ϴ´�� �����ּ���.\n������ ��Ƽ� ���������� ������ �ű� �� �־��.",
            "ī�带 ����� ¦�� �´� ī�带 �����ּ���.\n¦�� ���������� ������� ���ư� ������ϴ�.\nī�带 ��� �������� �������?",
            "��ǻ�Ϳ� ������������ �Ұſ���.\n������ ���ϴ´�� �̱�ų� ���ų� ����ּ���.\n*��ǻ�ʹ� ������ �̸� ����ϴ�.*",
            "����� ������ �׽�Ʈ �Ұſ���. ��ư�� ��ġ�� ����ϰų� ������ ���� ���� ���� ������ּ���.\n����� ������� ��ư�� ������ Ŭ����!"
        };
        imageNum = new int[titleNum] { 0, 1, 2, 3, 4 };
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
        //Debug.Log("Start Scene"+selectedMenu);
    }

    public class MenuSheet
    {
        public string title;
        public string description;
        public List<Sprite> sprites;

        public MenuSheet(string title, string description, List<Sprite> sprites)
        {
            this.title = title;
            this.description = description;
            this.sprites = sprites;
        }
    }
}
