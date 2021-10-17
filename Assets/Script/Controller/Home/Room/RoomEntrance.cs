using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntrance : Room
{
    public enum Status
    {
        InFromLeft,
        InFromRight,
        Wait,
        WalkToLeft,
        WalkToRight,
        happy,
        WalkToRandom ,
        Finish
    }
    private Status status;

    private LimitTimeCounter statusTime;

    private LimitTimeCounter sayDelayTime;

    private bool requestSayFirstHello;
    private LimitTimeCounter sayHelloWaitTime;

    private int roomIdNext;

    private float randomMoveTargetX, interval;


    public RoomEntrance()
    {
        this.roomIdNext = RoomData.RoomID_None;
    }


    /// <summary>
    /// 状態の設定します。
    /// </summary>
    /// <param name="status">変更する状態</param>
    private void StatusChange(Status status)
    {
        this.status = status;

        switch (this.status)
        {
            case Status.InFromLeft:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                break;
            case Status.InFromRight:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                break;
            case Status.Wait:
                this.kuma.AnimationStart(this.kuma.animation_hash_stand);

                if( !this.requestSayFirstHello )
                    this.statusTime.Start( Random.Range(1,5) );
                else
                    this.statusTime.Start(Random.Range(4, 6));


                if (this.roomIdNext == RoomData.RoomID_None && Random.Range(0,3)==0 )
                {
                    int roomIdNextCheck = this.director.ThinkRoomTarget();

                    if (roomIdNextCheck != RoomData.RoomID_Entrance && roomIdNextCheck != RoomData.RoomID_None)
                    {
                        this.roomIdNext = roomIdNextCheck;

                        if (this.roomIdNext == RoomData.RoomID_Living)
                        {
                        }
                        else if (this.roomIdNext == RoomData.RoomID_Bedroom)
                        {
                            if( !this.requestSayFirstHello )
                                this.kuma.Say(SpeechData.SpeechID_Spleepy);
                        }
                    }
                }

                break;
            case Status.WalkToLeft:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                break;
            case Status.WalkToRight:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);
                break;

            case Status.WalkToRandom:


                this.randomMoveTargetX = Random.Range(-2f, 2f);

                if ( this.kuma.X > this.randomMoveTargetX )
                    this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                else
                    this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);

                this.kuma.AnimationStart(this.kuma.animation_hash_walk);

                if (Random.Range(0, 2) == 0)
                    this.kuma.Say(SpeechData.SpeechID_Bored);
                break;

        }
    }

    /// <summary>
    /// 状態を更新します。
    /// </summary>
    private void StatusUpdate()
    {

        this.statusTime.Update();
        this.sayDelayTime.Update();
        //Debug.Log(this.status);
        switch (this.status)
        {
            case Status.InFromLeft:

                this.kuma.Move(0, Room.LineY_Kuma_ActiveLine, 1f);

                break;
            case Status.InFromRight:

                this.kuma.Move(0, Room.LineY_Kuma_ActiveLine, 1f);

                break;
            case Status.Wait:

                if( this.requestSayFirstHello && !this.sayHelloWaitTime.IsFinished )
                {
                    this.sayHelloWaitTime.Update();

                    if(this.sayHelloWaitTime.IsFinished)
                    {
                        this.kuma.Say(SpeechData.SpeechID_Hello);
                        this.requestSayFirstHello = false;
                    }
                }

                break;
            case Status.WalkToLeft:
                this.kuma.Move(Room.LineX_Kuma_OutLine_Left, Room.LineY_Kuma_ActiveLine, 1f);


                break;
            case Status.WalkToRight:
                this.kuma.Move(7, Room.LineY_Kuma_ActiveLine, 1f);
                break;

            case Status.WalkToRandom:
                this.kuma.Move( this.randomMoveTargetX , Room.LineY_Kuma_ActiveLine, 1f);

                break;
        }

    }


    private void StatusUpdateFinished()
    {

        int thinkRandom;

        switch (this.status)
        {
            case Status.InFromLeft:

                if (this.kuma.X > 0f)
                {
                    this.StatusChange(Status.Wait);
                }
                break;
            case Status.InFromRight:


                if (this.kuma.X < 0f)
                {
                    this.StatusChange(Status.Wait);
                }
                break;
            case Status.Wait:


                if (this.statusTime.IsFinished && kuma.GetIsSaying())
                {
                    this.sayDelayTime.Start(1);
                }
                else if (this.statusTime.IsFinished && !kuma.GetIsSaying() && this.sayDelayTime.IsFinished )
                {
                    if (this.roomIdNext == RoomData.RoomID_Living)
                    {
                        this.StatusChange(Status.WalkToLeft);
                    }
                    else if (this.roomIdNext == RoomData.RoomID_Bedroom)
                    {
                        this.StatusChange(Status.WalkToRight);
                    }
                    else
                    {
                        thinkRandom = Random.Range(0, 5);

                        if (thinkRandom <= 2)
                        {
                            this.StatusChange(Status.WalkToRandom);
                        }
                        else
                        {
                            this.StatusChange(Status.Wait);
                        }
                    }
                }

                break;
            case Status.WalkToLeft:

                if (this.kuma.X < Room.LineX_Kuma_OutLine_Left)
                {
                    this.director.MoveRoom(this.roomIdNext);
                    this.StatusChange(Status.Finish);
                }

                break;
            case Status.WalkToRight:

                if (this.kuma.X > Room.LineX_Kuma_OutLine_Right)
                {
                    this.director.MoveRoom(this.roomIdNext);
                    this.StatusChange(Status.Finish);
                }

                break;

            case Status.WalkToRandom:
                if ( (this.kuma.TurnDirection == KumaForRoom.TurnType.Right && this.kuma.X > this.randomMoveTargetX)
                  ||  (this.kuma.TurnDirection == KumaForRoom.TurnType.Left && this.kuma.X < this.randomMoveTargetX) )
                {
                    this.StatusChange(Status.Wait);
                }
                break;

            case Status.happy:
                interval += Time.deltaTime;
                if (interval > 1.5f)
                {
                    this.StatusChange(Status.Wait);
                    interval = 0f;
                }
                break;
        }

    }



    /// <summary>
    /// 部屋を作成   
    /// </summary>
    public override void RoomCreated()
    {
        this.statusTime = new LimitTimeCounter();
        this.sayDelayTime = new LimitTimeCounter();
        this.sayHelloWaitTime = new LimitTimeCounter();

        this.backgroundRenderer.sprite = this.director.Resource.SpriteRoomBack_Entrance;

        this.requestSayFirstHello = false;

        if (this.director.RoomID_Last == RoomData.RoomID_Living)
        {
            this.kuma.SetPosition(Room.LineX_Kuma_OutLine_Left, Room.LineY_Kuma_ActiveLine);
            this.StatusChange(Status.InFromLeft);
        }
        else if (this.director.RoomID_Last == RoomData.RoomID_Bedroom)
        {
            this.kuma.SetPosition(Room.LineX_Kuma_OutLine_Right, Room.LineY_Kuma_ActiveLine);
            this.StatusChange(Status.InFromRight);
        }
        else
        {
            this.requestSayFirstHello = true;
            this.sayHelloWaitTime.Start(2f);

            this.kuma.SetPosition(0f, Room.LineY_Kuma_ActiveLine);
            this.StatusChange(Status.Wait);

            //this.kuma.Say(SpeechData.SpeechID_Hello);
        }

    }

    public override void RoomDestroyed()
    {

    }

    public override void RoomUpdate()
    {
        this.StatusUpdate();
    }

    public override void RoomUpdateFinished()
    {
        this.StatusUpdateFinished();
    }

    private void GotoLiving()
    {
        this.roomIdNext = RoomData.RoomID_Living;
        this.StatusChange(Status.WalkToLeft);
    }

    private void GotoBedroom()
    {
        this.roomIdNext = RoomData.RoomID_Bedroom;
        this.StatusChange(Status.WalkToRight);
    }

    public override bool RunDebugCommand(string[] cmds, IDebugCommandListener commandListener)
    {
        if (cmds.Length >= 2 && cmds[0].Equals("goto"))
        {
            if (cmds[1].Equals("entrance"))
                commandListener.Error(cmds, "既に Entrance にいます。");
            else if (cmds[1].Equals("living"))
            {
                commandListener.Success(cmds);
                this.GotoLiving();
            }
            else if (cmds[1].Equals("bedroom"))
            {
                commandListener.Success(cmds);
                this.GotoBedroom();
            }

            return true;
        }

        return false;
    }

    public override void RunDebugStatus(IDebugCommandListener commandListener)
    {
        string message = "";
        message += "Room Entrance\n";
        message += "\tStatus " + this.status.ToString() + "\n";
        message += "\tNext Room " + this.roomIdNext + "\n";



        commandListener.Message(message);
        return;
    }

    public override void StopStatus()
    {
        this.status = Status.happy;

    }
    
}
