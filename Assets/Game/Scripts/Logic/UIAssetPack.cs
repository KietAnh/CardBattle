using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAssetPack
{
    public const string texUIBundleName = "tex_ui";

    public static void LoadPack()
    {
        AssetLoadManager.PreloadAssetBundle(texUIBundleName);  
    }
    public static void SetImage(Image image, string spriteName, bool setNativeSize = true)
    {
        Texture2D texture = AssetLoadManager.LoadAsset<Texture2D>(texUIBundleName, spriteName);
        if (texture == null)
        {
            Debug.LogError("Cannot load texture " + spriteName);
            return;
        }
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;
        if (setNativeSize)
        {
            image.SetNativeSize();
        }
    }

    
}
