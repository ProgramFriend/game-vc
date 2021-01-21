using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickController : MonoBehaviour
{
    public float timeLimit = 0.25f;
    public PointerEventData.InputButton button;

    [System.Serializable]
    public class OnSingleClick : UnityEvent { };
    public OnSingleClick onSingleClick;

    [System.Serializable]
    public class OnDoubleClick : UnityEvent { };
    public OnDoubleClick onDoubleClick;

    private int clickCount;
    private float firstClickTime;
    private float currentTime;

    private ClickController()
    {
        clickCount = 0;
    }

    public void onClick(BaseEventData data)
    {

        PointerEventData pointerData = data as PointerEventData;

        if (this.button != pointerData.button)
        {
            return;
        }

        this.clickCount++;

        if (this.clickCount == 1)
        {
            firstClickTime = pointerData.clickTime;
            currentTime = firstClickTime;
            StartCoroutine(ClickRoutine());
        }
    }

    private IEnumerator ClickRoutine()
    {

        while (clickCount != 0)
        {
            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;

            if (currentTime >= firstClickTime + timeLimit)
            {
                if (clickCount == 1)
                {
                    onSingleClick.Invoke();
                    Debug.Log("Single click");
                }
                else
                {
                    onDoubleClick.Invoke();
                }
                clickCount = 0;
            }
        }
    }
}