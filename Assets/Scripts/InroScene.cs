using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InroScene : MonoBehaviour
{
    public RawImage intro1;
    public RawImage intro2;
    public RawImage intro3;
    public RawImage intro4;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    void Start()
    {
        StartCoroutine("FadeImageOne");
    }

    IEnumerator FadeImageOne()
    {
        RawImage image = (intro1.GetComponent<RawImage>());
        float transparency = intro1.color.a;

        yield return new WaitForSeconds(2);
        while (transparency < 1)
        {
            transparency += 0.1f;
            intro1.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        while (transparency > 0)
        {
            transparency -= 0.1f;
            intro1.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        image1.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine("FadeImageTwo");
    }

    IEnumerator FadeImageTwo()
    {
        image2.SetActive(true);
        RawImage image = (intro2.GetComponent<RawImage>());
        float transparency = intro2.color.a;

        while (transparency < 1)
        {
            transparency += 0.1f;
            intro2.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        while (transparency > 0)
        {
            transparency -= 0.1f;
            intro2.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        image2.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine("FadeImageThree");
    }

    IEnumerator FadeImageThree()
    {
        image3.SetActive(true);
        RawImage image = (intro3.GetComponent<RawImage>());
        float transparency = intro3.color.a;

        while (transparency < 1)
        {
            transparency += 0.1f;
            intro3.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        while (transparency > 0)
        {
            transparency -= 0.1f;
            intro3.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        image3.SetActive(false);
        yield return new WaitForSeconds(1);
        StartCoroutine("FadeImageFour");
    }

    IEnumerator FadeImageFour()
    {
        image4.SetActive(true);
        RawImage image = (intro4.GetComponent<RawImage>());
        float transparency = intro4.color.a;

        while (transparency < 1)
        {
            transparency += 0.1f;
            intro4.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2);
        while (transparency > 0)
        {
            transparency -= 0.1f;
            intro4.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }
        image4.SetActive(false);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
