using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pad : MonoBehaviour
{
    [SerializeField]
    Shadow shadow;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    Image btnImage;
    public TMP_Text txtNumber;

    private void Start()
    {
        
        //txtNumber.rectTransform =
        btnImage.sprite = sprites[0];
    }

    public void LightSwitch(float speed)
    {
        StartCoroutine(LightUpDown(speed));
    }

    IEnumerator LightUpDown(float speed)
    {
        btnImage.sprite = sprites[1];
        yield return new WaitForSeconds(speed);
        btnImage.sprite = sprites[0];
    }


}
