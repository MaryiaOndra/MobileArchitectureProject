using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1f;
        }

        public void Hide() => StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (curtain.alpha > 0f)
            {
                curtain.alpha -= 0.3f;
                yield return new WaitForSeconds(0.3f);
            }
            gameObject.SetActive(false);
        }
    }
}
