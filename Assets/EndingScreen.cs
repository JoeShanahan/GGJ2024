using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EndingScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] _text;

    [SerializeField]
    private TMP_Text _finText;

    [SerializeField]
    private CanvasGroup _group;

    private void Start()
    {
        _group.alpha = 0;
    }
    
    public void ShowEnding(EndingData ending)
    {
        StartCoroutine(AnimateRoutine(ending));
    }

    private IEnumerator AnimateRoutine(EndingData ending)
    {
        int numLines = ending.EndingText.Length;

        for (int i=0; i<_text.Length; i++)
        {
            _text[i].color = new Color(1, 1, 1, 0);
            _text[i].gameObject.SetActive(i < numLines);
            
            if (_text[i].gameObject.activeSelf)
            {
                _text[i].text = ending.EndingText[i];
            }
        }

        _group.alpha = 0;
        _group.DOFade(1, 1);
        _finText.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(1f);
    

        for (int i=0; i<numLines; i++)
        {
            _text[i].DOFade(1, 1.5f);
            yield return new WaitForSeconds(4);
        }

        yield return new WaitForSeconds(1);

        _finText.DOFade(1, 1);
    }
}
