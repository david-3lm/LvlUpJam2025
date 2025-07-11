#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class ModelThumbnail
{
    public static Texture2D GetPreview(GameObject model)
    {
        Texture2D thumb = AssetPreview.GetAssetPreview(model);

        if (thumb == null)
        {
            AssetPreview.SetPreviewTextureCacheSize(1);
            AssetPreview.GetAssetPreview(model);
            AssetPreview.GetAssetPreview(model);
        }

        return thumb;
    }
}
#endif
