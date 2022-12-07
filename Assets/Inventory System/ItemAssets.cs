using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public Sprite crystalSprite;
    public Sprite ticketSprite;
    public Sprite coconutSprite;
    public Sprite bottleSprite;
    public Sprite woodSprite;
    public Sprite fishSprite;
}
