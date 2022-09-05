using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Pad : MonoBehaviour
{
    [SerializeField]
    Shadow shadow;
    [SerializeField]
    Image fillImage;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    Image btnImage;


    private void Start()
    {
        fillImage.fillAmount = 0;
        btnImage.sprite = sprites[0];
    }

    public void LightSwitch()
    {



        StartCoroutine(LightUpDown());
    }

    IEnumerator LightUpDown()
    {
        btnImage.sprite = sprites[1];
        yield return new WaitForSeconds(0.5f);
        btnImage.sprite = sprites[0];


        //fillImage.fillOrigin = 1; /*(int)Image.OriginVertical.Top;*/
        //while (fillImage.fillAmount < 1)
        //    {
        //        yield return null;
        //        fillImage.fillAmount += Time.deltaTime * 2;
        //    }
        //fillImage.fillOrigin = 0; /*(int)Image.OriginVertical.Bottom;*/
        //while (fillImage.fillAmount > 0)
        //{
        //    yield return null;
        //    fillImage.fillAmount -= Time.deltaTime * 2;
        //}
    }
}
