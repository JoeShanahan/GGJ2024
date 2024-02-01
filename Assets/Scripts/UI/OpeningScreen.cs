using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class OpeningScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _text1;
    [SerializeField] private TMP_Text _text2;
    [SerializeField] private TMP_Text _text3;
    [SerializeField] private TMP_Text _text4;
    [SerializeField] private CanvasGroup _group;

    // Start is called before the first frame update
    void Start()
    {
        _group.alpha = 1;
        _text1.color = _text2.color = _text3.color = _text4.color = new Color(1, 1, 1, 0);
        StartCoroutine(IntroSequence());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            DOTween.Kill(_group);
            _group.DOFade(0, 0.4f).OnComplete(() => Destroy(gameObject));
        }
    }

    private IEnumerator IntroSequence()
    {
        yield return new WaitForSeconds(0.5f);

        _text1.DOFade(1, 1.5f);

        yield return new WaitForSeconds(1.5f);

        _text2.DOFade(1, 1.5f);

        yield return new WaitForSeconds(3);

        _text1.DOFade(0, 0.5f);
        _text2.DOFade(0, 0.5f);

        yield return new WaitForSeconds(1);

        _text3.DOFade(1, 1.5f);

        yield return new WaitForSeconds(1.5f);

        _text4.DOFade(1, 1.5f);

        yield return new WaitForSeconds(3f);

        _text3.DOFade(0, 0.5f);
        _text4.DOFade(0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        _group.DOFade(0, 1).OnComplete(() => Destroy(gameObject));
    }
}
