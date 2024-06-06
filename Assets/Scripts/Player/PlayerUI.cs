using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _aimImage;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Image _imageWeapon;
    [SerializeField] private Image _imageDamage;
    [SerializeField] private Text _textInfo;
    private Sequence _sequenceAimAnimationShot;
    public void AimAnimationShot()
    {
        float scaleDefolt = 1;
        float scaleShot = 2f;
        float timeAnimIncrease = 0.2f;
        float timeAnimDecrease = 0.1f;

        _sequenceAimAnimationShot.Kill();
        _sequenceAimAnimationShot = DOTween.Sequence();
        _sequenceAimAnimationShot.Append(_aimImage.transform.DOScale(scaleShot, timeAnimIncrease));
        _sequenceAimAnimationShot.Append(_aimImage.transform.DOScale(scaleDefolt, timeAnimDecrease));
    }

    public void OutputHealth(float health)
    {
        _healthImage.fillAmount = health;

       // Debug.Log(health);

        if (health < 0)
            return;

        //Индикатор урона
        //if (health <= 0.5f)
        //{
        //    float valueFade = (1 - health);

        //    if (valueFade > 0.8f)
        //        return;

        //    _imageDamage.DOFade(valueFade, 0);
        //}
    }

    public void SetSpriteWreapon(Sprite sprite)
    {
        _imageWeapon.sprite = sprite;
        _imageWeapon.SetNativeSize();
    }

    public void OutputInfo(string textInfo)
    {
        _textInfo.text = textInfo;
    }
}
