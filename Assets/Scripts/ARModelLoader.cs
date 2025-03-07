using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ARModelLoader : MonoBehaviour
{
    private void Start()
    {
        string modelURL = PlayerPrefs.GetString("SelectedModelURL", "");
        if (!string.IsNullOrEmpty(modelURL))
        {
            StartCoroutine(DownloadAndLoadModel(modelURL));
        }
        else
        {
            Debug.LogError("No model URL found in PlayerPrefs.");
        }
    }

    private IEnumerator DownloadAndLoadModel(string url)
    {
        Debug.Log("Downloading AssetBundle from: " + url);

        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error while downloading Asset Bundle: " + request.error);
                yield break;
            }

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            if (bundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                yield break;
            }

            // Load the first asset in the bundle
            string[] assetNames = bundle.GetAllAssetNames();
            Debug.Log("Assets in bundle: " + string.Join(", ", assetNames));

            GameObject modelPrefab = bundle.LoadAsset<GameObject>(assetNames[0]); // Load first asset
            if (modelPrefab != null)
            {
                GameObject model = Instantiate(modelPrefab);
                model.transform.position = Vector3.zero;
                model.transform.localScale = Vector3.one;

                ARPlacement.Instance.AR_Model = model;
                Debug.Log("Model loaded successfully!");
            }
            else
            {
                Debug.LogError("Failed to instantiate model.");
            }

            bundle.Unload(false); // Unload the bundle but keep the assets loaded
        }
    }
}
