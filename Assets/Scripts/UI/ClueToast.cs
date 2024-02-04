using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class ClueToast : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    
    [SerializeField]
    private TMP_Text _titleText;

    [SerializeField]
    private TMP_Text _bodyText;

    [SerializeField]
    private RectTransform _thisRect;

    [SerializeField]
    private CanvasGroup _thisGroup;

    [Header("Timings")]
    [SerializeField]
    private float _transitionTime;

    [SerializeField]
    private float _visibleTime;

    private Queue<ItemData> _toastQueue = new();
    private bool _isBusy;

    private void Start()
    {
        _thisGroup.alpha = 0;
        _thisRect.DOAnchorPosX(_thisRect.sizeDelta.x, 0);
    }

    public void ShowFoundClue(ItemData dat)
    {
        _toastQueue.Enqueue(dat);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBusy)
            return;

        if (_toastQueue.Count > 0)
        {
            StartCoroutine(ToastRoutine(_toastQueue.Dequeue()));
        }
    }

    private IEnumerator ToastRoutine(ItemData data)
    {
        _isBusy = true;
        _icon.sprite = data.Icon;
        _titleText.text = data.ItemName;
        _bodyText.text = data.ShortDescription;

        _thisGroup.alpha = 0;
        _thisRect.DOAnchorPosX(0, _transitionTime);
        _thisGroup.DOFade(1, _transitionTime / 2);

        yield return new WaitForSeconds(_transitionTime + _visibleTime);

        _thisRect.DOAnchorPosX(_thisRect.sizeDelta.x, _transitionTime);
        _thisGroup.DOFade(0, _transitionTime);

        yield return new WaitForSeconds(_transitionTime + 0.15f);

        _isBusy = false;
    }
}
