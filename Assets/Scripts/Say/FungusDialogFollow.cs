using UnityEngine;

public class FungusDialogFollow : MonoBehaviour
{
    public Transform target; // NPC 的 HeadAnchor
    public Vector3 offset;   // 偏移量，用於微調對話框位置
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            Debug.LogError("Canvas 必須設置為 Screen Space - Overlay 模式！");
        }
    }

    void Update()
    {
        if (target != null && canvas != null)
        {
            // 將世界座標轉換為螢幕座標
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);

            // 更新對話框的 UI 位置
            rectTransform.position = screenPos;
            if (screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                rectTransform.gameObject.SetActive(false);
            }
            else
            {
                rectTransform.gameObject.SetActive(true);
            }

        }

    }
}

