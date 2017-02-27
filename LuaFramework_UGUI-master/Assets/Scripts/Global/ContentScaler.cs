using UnityEngine;
using System.Collections;

public enum ScaleType
{
	None,
	WidthFix
}
public class ContentScaler : MonoBehaviour 
{
    static float UI_WIDTH = 1280f;
    static float UI_HEIGHT = 720f;

    public ScaleType scaleType = ScaleType.None;

    private float scale = 1f;
    public float Scale
    {
        get
        {
            return scale;
        }
    }

    void Start()
    {
        FixScale();
    }

    public void FixScale()
    {
        float rootScale = (float)Screen.height / UI_HEIGHT;
        float widthScale = (float)Screen.width / UI_WIDTH;

        switch(scaleType)
        {
            case ScaleType.WidthFix:
                this.scale = widthScale/rootScale;
                break;
            default:
                this.scale = 1;
                break;
        }
		//设置默认宽高1280，720，算出缩放的比率赋值给RectTransform
        GetComponent<RectTransform>().localScale = new Vector3(Scale, Scale, 1);
    }
}