using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public GameObject imagePrefab;
    public List<GameObject> imageObjects;
    public Texture2D[] images;
    public List<string> imageTypes;

    private void Awake()
    {
        images = Resources.LoadAll<Texture2D>("Images");
        
        foreach (var image in images)
        {
            GameObject imageObject = Instantiate(imagePrefab);
            imageObjects.Add(imageObject);
            imageObject.AddComponent<ImageData>();

            string[] imageData = image.name.Split("_");

            imageObject.GetComponent<ImageData>().type = imageData[0];
            imageObject.GetComponent<ImageData>().isReal = imageData[1] == "real";

            imageObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = image;
            imageObject.SetActive(false);
            
            if(!imageTypes.Contains(imageData[0])) imageTypes.Add(imageData[0]);
        }
    }

    public GameObject[] LoadImagePair()
    {
        GameObject[] imagePair = new GameObject[2];
        
        if (imageObjects.Count < 2) return imagePair;
        int randomType = Random.Range(0, imageTypes.Count);
        string type = imageTypes[randomType];
        
        int index = 0;
        
        foreach (var imageObject in imageObjects)
        {
            if (imageObject.GetComponent<ImageData>().type == type)
            {
                imagePair[index] = imageObject;
                index++;
            }
        }

        imageObjects.Remove(imagePair[0]);
        imageObjects.Remove(imagePair[1]);
        imageTypes.Remove(type);

        int randomChanceForPlacement = Random.Range(0, 2);
        if (randomChanceForPlacement == 1)
        {
            GameObject[] tempImagePair = new GameObject[2];
            tempImagePair[0] = imagePair[1];
            tempImagePair[1] = imagePair[0];
            imagePair = tempImagePair;
        }

        return imagePair;
    }
}
