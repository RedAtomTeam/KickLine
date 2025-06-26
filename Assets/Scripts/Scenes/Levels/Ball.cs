using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private List<StoreStuff> _ballStuff;
    [SerializeField] private Image _ballImage;

    [SerializeField] private TextMeshProUGUI? _debugText;
    
    private Rigidbody2D _rb;
    private bool _isActivate = false; 



    public void Activate()
    {
        _isActivate = true;   
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        foreach (var ball in _ballStuff)
        {
            if (ball.isSelected)
            {
                _ballImage.sprite = ball._sprite;
                break;
            }
        }
    }

    private void Update()
    {

        string text = "Update ";

        if (_isActivate)
        {
            text = text + "IsActive ";

            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {

                if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButton(0))
                {
                    text = text + "Touch ";
                    _isActivate = false;

                    Vector2 touchWorldPos = Vector2.zero;
                    if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                    {
                        text = text + "Began ";
                        touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    }
                    if (Input.GetMouseButton(0))
                    {
                        touchWorldPos = Input.mousePosition;
                    }

                    Vector2 direction = (touchWorldPos - (Vector2)transform.position).normalized;
                    _rb.AddForce(direction * _force, ForceMode2D.Impulse);
                }
            }
        }

        //if (_debugText != null)
        //{
        //    _debugText.text = _debugText.text.Length <= text.Length ? text : _debugText.text;
        //}

    }

}
