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
            "<rainb f=0.2>손으로 사칙연산 게임</rainb>",
            "<rainb f=0.2>성냥 옮기기 게임</rainb>",
            "<rainb f=0.2>카드 뒤집기 게임</rainb>",
            "<rainb f=0.2>가위 바위 보</rainb>",
            "<rainb f=0.2>기억력 테스트</rainb>"
        };
        descriptions = new string[titleNum] {
            "화면에는 펼친 손가락 갯수만큼 숫자가 표시돼요.\n화면에 나온 덧셈 뺄셈 곱셈 나눗셈 문제의 답을\n손가락을 펼쳐서 맞춰주세요.\n",
            "성냥개비로 모양이나 숫자를 만들었어요.\n성냥개비를 옮겨서 문제가 말하는대로 고쳐주세요.\n손으로 잡아서 목적지에서 놓으면 옮길 수 있어요.",
            "카드를 뒤집어서 짝이 맞는 카드를 맞춰주세요.\n짝이 맞지않으면 원래대로 돌아가 버린답니다.\n카드를 모두 뒤집을수 있을까요?",
            "컴퓨터와 가위바위보를 할거예요.\n문제가 말하는대로 이기거나 지거나 비겨주세요.\n*컴퓨터는 언제나 미리 낸답니다.*",
            "당신의 기억력을 테스트 할거예요. 버튼의 위치를 기억하거나 구슬이 내는 빛의 색을 기억해주세요.\n기억한 순서대로 버튼을 누르면 클리어!"
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
