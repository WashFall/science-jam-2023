using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public GameObject imagePrefab;
    public List<GameObject> imageObjects;
    public Texture2D[] images;

    private void Start()
    {
        foreach (var image in images)
        {
            GameObject imageObject = Instantiate(imagePrefab);
            imageObjects.Add(imageObject);
            imageObject.AddComponent<ImageData>();

            string[] imageData = image.name.Split("_");

            imageObject.GetComponent<ImageData>().type = imageData[0];
            imageObject.GetComponent<ImageData>().isReal = imageData[1] == "real";

            imageObject.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTexture = image;
            imageObject.SetActive(false);
        }
    }
}
