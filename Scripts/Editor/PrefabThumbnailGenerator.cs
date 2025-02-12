using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour))]
public class PrefabThumbnailGenerator : Editor
{
    [MenuItem("Assets/Create Prefab Thumbnail")]
    private static void CreatePrefabThumbnail()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(selectedObject));

            if (prefabPath != null && prefabPath.EndsWith(".prefab"))
            {
                Texture2D thumbnail = AssetPreview.GetAssetPreview(selectedObject);

                if (thumbnail != null)
                {
                    string thumbnailPath = prefabPath.Replace(".prefab", "_thumbnail.png");
                    byte[] bytes = thumbnail.EncodeToPNG();
                    System.IO.File.WriteAllBytes(thumbnailPath, bytes);

                    Debug.Log("Thumbnail created at: " + thumbnailPath);
                    AssetDatabase.Refresh();
                }
                else
                {
                    Debug.LogWarning("Could not generate thumbnail for the selected prefab.");
                }
            }
            else
            {
                Debug.LogWarning("Please select a valid prefab.");
            }
        }
    }
}