using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Handles timing for Text Changes & Animation Starts

public class BonusController : MonoBehaviour
{
    public Text UIText1;
    public Text UIText2;

    public GameObject LeftConfetti;
    public GameObject RightConfetti;

    public Animator SignAnim;
    public Animator ZombiePillarAnim;
    public Animator SceneFaderAnim;

    private float timer = 0;
    private float textAddTime = 2f;

    private float fadeOutBoxTime = 13;
    private float signDropTime = 14f;
    private float zombiesRiseTime = 15f;
    private float confettiTime = 18f;
    private float endTime = 29.5f;
    private int stage = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fadeOutBoxTime && stage == 0)
        {
            stage++;
            SceneFaderAnim.Play("BlackCubeFadeOut");
        }
        if (timer >= signDropTime && stage == 1)
        {
            stage++;
            SignAnim.Play("BonusSignDrop");
        } else if (timer >= zombiesRiseTime && stage==2)
        {
            stage++;
            ZombiePillarAnim.Play("BonusStageRise");
        } else if (timer >= confettiTime && stage==3)
        {
            stage++;
            LeftConfetti.SetActive(true);
            RightConfetti.SetActive(true);
        }

        if (timer > endTime && stage == 4)
        {
            stage++;
            StartCoroutine(EndingFade(GetComponent<AudioSource>(), 2, 0, SceneFaderAnim));
        }
    }

    public static IEnumerator EndingFade(AudioSource audioSource, float duration, float targetVolume, Animator SceneFaderAnim)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        SceneFaderAnim.Play("BlackCubeFadeIn");

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        SceneManager.LoadScene("SampleScene");
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }
}
