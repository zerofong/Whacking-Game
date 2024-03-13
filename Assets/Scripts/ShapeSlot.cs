using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShapeSlot : MonoBehaviour, IPointerClickHandler
{
    public ShapeType type;
    public Image image;
    public float hitFadeDuration = 0.1f;

    private Coroutine coroutine;
    private bool canAddScore = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canAddScore) return;

        Hit();

        ScoreManager.OnScoreUpdateAction(type);
    }

    private void Hit()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        canAddScore = false;
        coroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = image.color;

        while (elapsedTime < hitFadeDuration)
        {
            float newAlpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / hitFadeDuration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure alpha is exactly 0 at the end of the fade and change type at last to make sure it wont get change immediately after hit
        ChangeShape(ShapeType.NONE);
        coroutine = null;
    }

    private void Disappear()
    {
        coroutine = StartCoroutine(DisappearTimer());
    }

    private IEnumerator DisappearTimer()
    {
        yield return new WaitForSeconds(1f);
        ChangeShape(ShapeType.NONE);
        canAddScore = false;
    }

    public void ChangeShape(ShapeType _type, Sprite sprite = null)
    {
        type = _type;
        image.sprite = sprite;
        
        if(type == ShapeType.NONE)
        {
            image.color = new Color(1, 1, 1, 0);
        }
        else
        {
            canAddScore = true;
            image.color = new Color(1, 1, 1, 1);
            Disappear();
        }
    }
}

public enum ShapeType { NONE, SQUARE, CIRCLE, TRIANGLE}