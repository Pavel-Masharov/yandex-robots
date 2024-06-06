using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DotsLoading : MonoBehaviour
{
    private Sequence _sequence;

    [SerializeField] private Image _dot1, _dot2, _dot3;
    void Start()
    {
        //StartAnim();
    }

    public void StartAnim()
    {


        float time = 0.5f;

        _sequence = DOTween.Sequence();

        _sequence.Append(_dot1.DOFade(1, time));
        _sequence.Append(_dot1.DOFade(0, time));
        _sequence.Join(_dot2.DOFade(1, time));

        _sequence.Append(_dot2.DOFade(0, time));
        _sequence.Join(_dot3.DOFade(1, time));
        _sequence.Append(_dot3.DOFade(0, time));

        _sequence.SetLoops(-1);
    }
}
