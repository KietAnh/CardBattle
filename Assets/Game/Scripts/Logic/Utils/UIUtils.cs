using UnityEngine;
using Data.Beans;
using UnityEngine.UI;

public static class UIUtils
{
    public static string GetStrByLanguageID(int id)
    {
        string lang = ConfigBean.GetBean<t_textBean, int>(id).t_content;
        if (lang == null)
        {
            return "" + id;
        }
        return lang;
    }
    public static string GetStrByConstantID(string id)
    {
        int textId = ConfigBean.GetBean<t_global_constantBean, string>(id).t_int_param;
        return GetStrByLanguageID(textId);
    }
    public static string GetStrFormat(int id, object param)
    {
        string str = GetStrByLanguageID(id);
        return string.Format(str, param);
    }
    public static string GetStrFormat(int id, object param1, object param2)
    {
        string str = GetStrByLanguageID(id);
        return string.Format(str, param1, param2);
    }

    public static void SetTextColor(Text text, string hexa)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hexa, out color);
        if (color != null)
            text.color = color;
    }

    public static void SetRectPosition(RectTransform rect, float x, float y)
    {
        rect.anchoredPosition = new Vector2(x, y);
    }
    public static void SetRectPositionX(RectTransform rect, float x)
    {
        rect.anchoredPosition = new Vector2(x, rect.anchoredPosition.y);
    }
    public static void SetRectPositionY(RectTransform rect, float y)
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
    }

    //
    public static float FindValidCanvasScalerMatchValue1(int screenWidth, int screenHeight, int refScreenWidth, int refScreenHeight)
    {
        float aspectRatio = screenHeight / (screenWidth * 1.0f);
        float refAspectRatio = refScreenHeight / (refScreenWidth * 1.0f);
        if (aspectRatio > refAspectRatio)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    public static float FindValidCanvasScalerMatchValue2(int screenWidth, int screenHeight, int refScreenWidth, int refScreenHeight)
    {
        for (float matchValue = 0f; matchValue < 1.1f; matchValue += 0.1f)
        {
            float scaleFactor = screenWidth / (refScreenWidth * 1.0f) * (1 - matchValue) + screenHeight / (refScreenHeight * 1.0f) * matchValue;
            if (refScreenWidth * scaleFactor >= screenWidth && refScreenHeight * scaleFactor >= screenHeight)
                return matchValue;
        }
        return 0;
    }
}
