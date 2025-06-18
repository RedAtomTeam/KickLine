using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _force;
    
    private Rigidbody2D _rb;
    private bool _isStart = false;
    private bool _isEnable = false;

    private void OnEnable()
    {
        _isEnable = true;   
    }

    private void Awake()
    {
        _isStart = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isEnable)
        {
            if (!_isStart)
            {
                if (Input.touchCount > 0 || Input.GetMouseButton(0))
                {
                    _isStart = true;
                    print("Click");

                    if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButton(0))
                    {
                        Vector2 touchWorldPos = Vector2.zero;
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
                        {
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
        }
    }

}
