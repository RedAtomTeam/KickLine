using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private float _defaultScaleX;
    private RectTransform _rectTransform;

    [SerializeField] private RectTransform parentBounds; 
    [SerializeField] private Collider2D movementArea;    
    [SerializeField] private float dragSpeed = 1f;

    [SerializeField] private AudioClip _kickClip;

    private AudioService _audioService;
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

        _audioService = AudioService.Instance;
    }

    private void Update()
    {   
        if (_rectTransform.anchoredPosition.x > 0)
            _rectTransform.localScale = new Vector3(-_defaultScaleX, 1, 1);
        else
            _rectTransform.localScale = new Vector3(_defaultScaleX, 1, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isInterract)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out offset
            );
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isInterract)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentBounds,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localCursor
            ))
            {
                Vector2 newPos = localCursor - offset;

                newPos.x = Mathf.Clamp(
                    newPos.x,
                    parentBounds.rect.xMin + rectTransform.rect.width / 2,
                    parentBounds.rect.xMax - rectTransform.rect.width / 2
                );
                newPos.y = Mathf.Clamp(
                    newPos.y,
                    parentBounds.rect.yMin + rectTransform.rect.height / 2,
                    parentBounds.rect.yMax - rectTransform.rect.height / 2
                );

                rectTransform.anchoredPosition = newPos;
            }
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _audioService.PlayEffect(_kickClip);
        }
    }
}
