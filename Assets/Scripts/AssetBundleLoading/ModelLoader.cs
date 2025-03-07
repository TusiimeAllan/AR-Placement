using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class ModelLoader : MonoBehaviour
{
    public string modelListURL = "http://localhost/models.json"; // JSON file with model data
    public Transform gridParent; // Parent transform for the list items
    public GameObject modelPrefab; // Prefab for each UI item

    private void Start()
    {
        StartCoroutine(LoadModelList());
    }

    IEnumerator LoadModelList()
    {
        UnityWebRequest request = UnityWebRequest.Get(modelListURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            ModelList modelList = JsonUtility.FromJson<ModelList>(request.downloadHandler.text);
            
            foreach (var model in modelList.models)
            {
                GameObject item = Instantiate(modelPrefab, gridParent);
                item.GetComponentInChildren<TMP_Text>().text = model.name;
                
                StartCoroutine(LoadImage(model.thumbnailURL, item.transform.Find("Image").GetComponent<Image>()));

                string modelURL = model.modelURL; // Capture URL for lambda
                item.GetComponent<Button>().onClick.AddListener(() => LoadARScene(modelURL));
            }
        }
        else
        {
            Debug.LogError("Failed to load model list: " + request.error);
        }
    }

    IEnumerator LoadImage(string url, Image img)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
        else
        {
            Debug.LogError("Failed to load image: " + request.error);
        }
    }

    void LoadARScene(string modelURL)
    {
        PlayerPrefs.SetString("SelectedModelURL", modelURL); // Save model URL
        SceneManager.LoadScene("ARScene"); // Load the AR scene
    }
}

[System.Serializable]
public class ModelData
{
    public string name;
    public string thumbnailURL;
    public string modelURL;
}

[System.Serializable]
public class ModelList
{
    public ModelData[] models;
}
