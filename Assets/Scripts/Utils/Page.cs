using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Page : MonoBehaviour
{
    private const float OPEN_CLOSE_TIME = .3f;

    private CanvasGroup canvasGroup = default;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowCoroutine());
    }

    public void Close() => Close(true);
    public virtual void Close(bool isActive = true)
    {
        StartCoroutine(HideCoroutine(isActive));
    }

    protected virtual void OnOpen() { }

    protected virtual void OnClose() { }

    private IEnumerator ShowCoroutine()
    {
        for (var time = canvasGroup.alpha * OPEN_CLOSE_TIME; canvasGroup.alpha < 1; time += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha = time / OPEN_CLOSE_TIME;
        }
        canvasGroup.blocksRaycasts = true;
        OnOpen();
    }

    private IEnumerator HideCoroutine(bool isActive)
    {
        canvasGroup.blocksRaycasts = false;
        for (var time = canvasGroup.alpha * OPEN_CLOSE_TIME; canvasGroup.alpha > 0; time -= Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha = time / OPEN_CLOSE_TIME;
        }
        OnClose();
        if (!isActive) { gameObject.SetActive(false); }
    }

}
