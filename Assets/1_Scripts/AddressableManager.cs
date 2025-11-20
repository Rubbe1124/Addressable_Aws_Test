using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    [SerializeField] private AssetReferenceSprite flagSprite;
    [SerializeField] private Image flagImage;
    // Update is called once per frame

    private void Start()
    {
        flagSprite.LoadAssetAsync().Completed += (img) =>
        {
            var image = flagImage.GetComponent<Image>();
            image.sprite = img.Result;
        };
    }
}
