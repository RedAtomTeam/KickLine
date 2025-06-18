using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float _defaultScaleX;
    private RectTransform _rectTransform;

    [SerializeField] private RectTransform parentBounds; 
    [SerializeField] private Collider2D movementArea;    
    [SerializeField] private float dragSpeed = 1f;

    private RectTransform rectTransform;
    private Camera mainCamera;
    private Vector2 offset;

    private bool _isInterract = true;


    public void Diactivate()
    {
        _isInterract = false;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_isInterract)
        {
            if (Input.touchCount > 0 && IsTouchOnObject(Input.GetTouch(0)))
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchWorldPos = mainCamera.ScreenToWorldPoint(touch.position);

                transform.position = Vector2.Lerp(
                    transform.position,
                    GetClampedPosition(touchWorldPos),
                    dragSpeed * Time.deltaTime
                );
            }
        }
        
        if (_rectTransform.anchoredPosition.x > 0)
            _rectTransform.localScale = new Vector3(-_defaultScaleX, 1, 1);
        else
            _rectTransform.localScale = new Vector3(_defaultScaleX, 1, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            mainCamera,
            out offset
        );
        offset = rectTransform.anchoredPosition - offset;
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentBounds,
            eventData.position,
            mainCamera,
            out Vector2 localPoint
        ))
        {
            Vector2 clampedPosition = new Vector2(
                Mathf.Clamp(localPoint.x + offset.x,
                    parentBounds.rect.xMin + rectTransform.rect.width / 2,
                    parentBounds.rect.xMax - rectTransform.rect.width / 2),
                Mathf.Clamp(localPoint.y + offset.y,
                    parentBounds.rect.yMin + rectTransform.rect.height / 2,
                    parentBounds.rect.yMax - rectTransform.rect.height / 2)
            );

            rectTransform.anchoredPosition = clampedPosition;
        }
    }

    private bool IsTouchOnObject(Touch touch)
    {
        Vector2 touchWorldPos = mainCamera.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(touchWorldPos, Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    private Vector2 GetClampedPosition(Vector2 targetPosition)
    {
        if (movementArea != null)
        {
            Bounds bounds = movementArea.bounds;
            return new Vector2(
                Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x),
                Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y)
            );
        }
        return targetPosition;
    }

}
