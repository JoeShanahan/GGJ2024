using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PhaseChangeUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _thisRect;

    [SerializeField]
    private Text _phaseText;

    [SerializeField]
    private Text _mainText;

    [SerializeField]
    private Text _descText;

    private void Start()
    {
        _thisRect.localScale = new Vector3(1, 0, 1);
    }

    public void StartNewPhase(PhaseInfo info, int phaseNum)
    {
        _thisRect.localScale = new Vector3(1, 0, 1);

        _phaseText.text = $"Investigation phase {phaseNum}";
        _mainText.text = info.Title.ToUpper();
        _descText.text = info.SubTitle;

        StartCoroutine(AppearRoutine());
    }

    private IEnumerator AppearRoutine()
    {
        _thisRect.DOScaleY(1, 0.5f).SetEase(Ease.OutExpo);

        yield return new WaitForSeconds(3);

        _thisRect.DOScaleY(0, 0.5f).SetEase(Ease.InQuad);
    }
}
