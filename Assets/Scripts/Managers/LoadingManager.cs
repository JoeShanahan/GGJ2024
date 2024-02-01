using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Image _wipeImage;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        _wipeImage.fillOrigin = 0;

        _wipeImage.DOFillAmount(1, 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
            _wipeImage.fillOrigin = 1;
            _wipeImage.DOFillAmount(0, 0.25f).SetEase(Ease.InSine);
        });
    }
}
