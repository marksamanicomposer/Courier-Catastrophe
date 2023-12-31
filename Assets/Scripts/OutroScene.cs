using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroScene : MonoBehaviour
{
    public RawImage outro1;
    public GameObject image1;

    public GameObject text;
    public GameObject button;

    private AudioSource submitSource;


    void Start()
    {
        submitSource = GetComponent<AudioSource>();
        StartCoroutine("FadeImage");
    }

    IEnumerator FadeImage()
    {
        RawImage image = (outro1.GetComponent<RawImage>());
        float transparency = outro1.color.a;

        yield return new WaitForSeconds(1);
        while (transparency < 1)
        {
            transparency += 0.1f;
            outro1.color = new Color(255, 255, 255, transparency);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2);
        text.SetActive(true);
        button.SetActive(true);
    }

    public void RestartGame()
    {
        StartCoroutine("ResetGame");
    }

    IEnumerator ResetGame()
    {
        submitSource.Play();
        yield return new WaitForSeconds(submitSource.clip.length);
        if (MusicPlayer.Instance != null)
            Destroy(MusicPlayer.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
