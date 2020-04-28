using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeResource
{
    public Sprite SpriteRoomBack_Entrance { get; private set; }
    public Sprite SpriteRoomBack_Living { get; private set; }
    public Sprite SpriteRoomBack_Bedroom { get; private set; }

    public Sprite[] SpriteNumberParts { get; private set; }
    public Sprite[] SpriteCardIconArray { get; private set; }
    //public Sprite[] AmountsItemParts{ get; private set; }



    public AudioClip AudioSelect { get; private set; }
    public AudioClip AudioBGM { get; private set; }

    public HomeResource()
    {
        this.SpriteRoomBack_Entrance = Resources.Load<Sprite>("images/room/background/entrance");
        this.SpriteRoomBack_Living = Resources.Load<Sprite>("images/room/background/living");
        this.SpriteRoomBack_Bedroom = Resources.Load<Sprite>("images/room/background/bedroom_back");



        this.SpriteNumberParts = Resources.LoadAll<Sprite>("images/text/number/number_parts_white");
        //this.AmountsItemParts = Resources.LoadAll<Sprite>("images/sugoroku/item/amount_item_parts");

        this.SpriteCardIconArray = Resources.LoadAll<Sprite>("images/room/card/card_array");


        this.AudioSelect = Resources.Load("sound/select", typeof(AudioClip)) as AudioClip;

        this.AudioBGM = Resources.Load("sound/bgm_home", typeof(AudioClip)) as AudioClip;

    }



}
