using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeartClick : MonoBehaviour, IPointerClickHandler
{
    public int points = 1;

    public void OnPointerClick(PointerEventData eventData)
    {
        ScoreManager.instance.AddScore(points);

        Destroy(gameObject);
    }
}