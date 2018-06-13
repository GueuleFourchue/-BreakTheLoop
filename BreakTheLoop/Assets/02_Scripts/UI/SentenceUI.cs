using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Colorful;

public class SentenceUI : MonoBehaviour
{
    [Header("Timing")]
    public float textDuration;

    [Header("Audio")]
    public AudioSource SFX_Glitch;

    [Header("Drag & Drop")]
    public PlayerController player;
    public Text sentence;
    public Image background;
    public CanvasGroup crosshairAlpha;
    public GameObject sentenceCamera;
    public GameObject inGameCanvas;

    void Start()
    {
        player.enabled = false;
        StartCoroutine(SentenceAnim());
    }


    public IEnumerator SentenceAnim()
    {
        yield return new WaitForSeconds(1f);
        sentence.DOFade(1, 4f).SetEase(Ease.Linear);
        sentence.transform.DOScale(1.15f, 8f);

        //Fade Effects
        LensDistortionBlur disto = sentenceCamera.GetComponent<LensDistortionBlur>();
        DOTween.To(() => disto.Distortion, x => disto.Distortion = x, 0.7f, 4);

        AnalogTV tv = sentenceCamera.GetComponent<AnalogTV>();
        DOTween.To(() => tv.Distortion, x => tv.Distortion = x, -0.5f, 4);

        UnityStandardAssets.ImageEffects.BloomOptimized bloom = sentenceCamera.GetComponent<UnityStandardAssets.ImageEffects.BloomOptimized>();
        DOTween.To(() => bloom.intensity, x => bloom.intensity = x, 1f, 3);

        yield return new WaitForSeconds(textDuration);
        DOTween.To(() => disto.Distortion, x => disto.Distortion = x, 0f, 4);

        //End
        sentence.DOFade(0, 1f);

        Led led = sentenceCamera.GetComponent<Led>();
        DOTween.To(() => led.Scale, x => led.Scale = x, 5, 1);

        sentenceCamera.GetComponent<Glitch>().enabled = true;
        SFX_Glitch.Play();

        yield return new WaitForSeconds(1);

        inGameCanvas.SetActive(true);
        sentenceCamera.SetActive(false);
        background.DOFade(0, 3f);
        crosshairAlpha.DOFade(1, 3f);
        player.enabled = true;
    }

    
}
