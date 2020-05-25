using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugorokuResource
{

    public GameObject PrefabIconItemx2 { get; private set; }
    public GameObject PrefabItemCountPrinter { get; private set; }
    public GameObject PrefabTileEffect { get; private set; }

    public GameObject PrefabFlyingItem { get; private set; }

    public GameObject PrefabItemShadow { get; private set; }

    public GameObject PrefabItemWorkBase { get; private set; }

    public GameObject PrefabItemGetStar { get; private set; }

    public Sprite[] SpritesSpecialFrame { get; private set; }
    public Sprite[] SpritesSpecialMessage { get; private set; }
    public Sprite[] SpritesNumberParts { get; private set; }

    public AudioClip AudioSaiStart { get; private set; }
    public AudioClip AudioSaiStop { get; private set; }

    public AudioClip AudioSpecialRoll { get; private set; }
    public AudioClip AudioSpecialSelected { get; private set; }

    public AudioClip AudioCardGet { get; private set; }
    public AudioClip AudioSugorokuMove { get; private set; }

    public AudioClip AudioStageClear { get; private set; }
    public AudioClip AudioGameOver { get; private set; }

    public AudioClip AudioSpecialDiceFreeCount { get; private set; }
    public AudioClip AudioSpecialDiceFreeCountLast5 { get; private set; }

    public AudioClip AudioBGM { get; private set; }

    public AudioClip AudioItemGet { get; private set; }

    public List<MapObjectPrefab> mapObjectPrefabs;
    public SugorokuResource()
    {

        this.SpritesSpecialFrame = Resources.LoadAll<Sprite>("images/sugoroku/special/special_frame");
        this.SpritesSpecialMessage = Resources.LoadAll<Sprite>("images/sugoroku/special/special_message");
        this.SpritesNumberParts = Resources.LoadAll<Sprite>("images/text/number/number_parts_white");

        this.AudioSaiStart = Resources.Load("sound/sai_start", typeof(AudioClip)) as AudioClip;
        this.AudioSaiStop = Resources.Load("sound/sai_stop", typeof(AudioClip)) as AudioClip;
        this.AudioCardGet = Resources.Load("sound/card_get", typeof(AudioClip)) as AudioClip;
        this.AudioSugorokuMove= Resources.Load("sound/sugoroku_move", typeof(AudioClip)) as AudioClip;

        this.AudioStageClear = Resources.Load("sound/stage_clear", typeof(AudioClip)) as AudioClip;
        this.AudioGameOver = Resources.Load("sound/game_over", typeof(AudioClip)) as AudioClip;


        this.AudioSpecialRoll = Resources.Load("sound/special_roll", typeof(AudioClip)) as AudioClip;
        this.AudioSpecialSelected = Resources.Load("sound/special_selected", typeof(AudioClip)) as AudioClip;

        this.AudioSpecialDiceFreeCount = Resources.Load("sound/special_dice_free_count", typeof(AudioClip)) as AudioClip;
        this.AudioSpecialDiceFreeCountLast5 = Resources.Load("sound/special_dice_free_count_last5", typeof(AudioClip)) as AudioClip;


        this.AudioBGM = Resources.Load("sound/bgm_stage", typeof(AudioClip)) as AudioClip;

        this.AudioItemGet = Resources.Load("sound/item_get", typeof(AudioClip)) as AudioClip;


        this.PrefabIconItemx2 = Resources.Load("prefabs/special/ItemIcon_x2", typeof(GameObject)) as GameObject;
        this.PrefabItemCountPrinter = Resources.Load("prefabs/item/ItemCount", typeof(GameObject)) as GameObject;
        this.PrefabTileEffect = Resources.Load("prefabs/tile/TileEffect", typeof(GameObject)) as GameObject;
        this.PrefabFlyingItem = Resources.Load("prefabs/item/FlyingItem", typeof(GameObject)) as GameObject;
        this.PrefabItemShadow = Resources.Load("prefabs/item/Shadow", typeof(GameObject)) as GameObject;
        this.PrefabItemWorkBase = Resources.Load("prefabs/item/ItemWorkBase", typeof(GameObject)) as GameObject;
        this.PrefabItemGetStar = Resources.Load("prefabs/item/ItemGetStar", typeof(GameObject)) as GameObject;

        this.mapObjectPrefabs = new List<MapObjectPrefab>();
    }


    public GameObject GetMapObjectPrefab( int id )
    {
        foreach( MapObjectPrefab check in this.mapObjectPrefabs )
        {
            if (check.ObjectID == id)
                return check.Prefab;
        }

        GameObject loadedPrefab = (GameObject)Resources.Load("prefabs/tile/" + id.ToString() );

        if( loadedPrefab != null)
        {
            MapObjectPrefab data = new MapObjectPrefab( id , loadedPrefab );
            this.mapObjectPrefabs.Add(data);
        }

        return loadedPrefab;

    }
}
