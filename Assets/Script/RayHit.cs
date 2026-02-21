using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayHit : MonoBehaviour
{
    private Camera _cam;
    private TMP_Text _scoreText;
    private int _score;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject temp = hit.collider.gameObject;
                if (temp.CompareTag("Heart"))
                {
                    Destroy(temp);
                    _score++;
                    _scoreText.text = $"Score: {_score}";
                }
                else if (temp.CompareTag("BedHeart"))
                {
                    Time.timeScale = 0; 
                }
            }
        }
    }
}
