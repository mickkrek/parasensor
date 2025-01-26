using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ScaleGridLayout : MonoBehaviour
{
    private GridLayoutGroup _gridLayoutGroup;
	private RectTransform _rect;

	void Start ()
	{
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _rect = GetComponent<RectTransform> ();
        UpdateCellSize();
	}

	void OnRectTransformDimensionsChange ()
	{
        if (_gridLayoutGroup != null && _rect != null)
        {
            UpdateCellSize();
        }
    }

    private void UpdateCellSize()
    {
        float size = ( _rect.rect.height - (_gridLayoutGroup.padding.top + _gridLayoutGroup.padding.bottom) ) / _gridLayoutGroup.constraintCount;
        _gridLayoutGroup.cellSize = new Vector2 (size, size);
    }
}
