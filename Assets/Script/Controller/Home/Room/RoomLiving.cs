using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLiving : Room
{
    public enum Status
    {
        In ,
        Wait ,
        WalkToLeft , 
        WalkToRight ,
        SitIn,
        Sit ,
        SitOut,
        Finish
    }
    private Status status;

    private LimitTimeCounter statusTime;

    private LimitTimeCounter sayDelayTime;

    private int roomIdNext;


    public RoomLiving()
    {
        this.roomIdNext = RoomData.RoomID_None;
    }


    /// <summary>
    /// 状態の設定します。
    /// </summary>
    /// <param name="status">変更する状態</param>
    private void StatusChange( Status status )
    {
        this.status = status;


        switch( this.status )
        {
            case Status.In:
                this.kuma.SetPosition(Room.LineX_Kuma_OutLine_Right, Room.LineY_Kuma_ActiveLine );
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);

                break;
            case Status.Wait:
                this.kuma.AnimationStart(this.kuma.animation_hash_stand);
                this.statusTime.Start(3f);

                break;
            case Status.WalkToLeft:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                break;
            case Status.WalkToRight:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);
                break;

            case Status.SitIn:
                this.kuma.AnimationStart(this.kuma.animation_hash_sitin);
                break;

            case Status.Sit:
                this.statusTime.Start(Random.Range(15, 30));
                this.kuma.AnimationStart(this.kuma.animation_hash_sitactive);


                int roomIdNextCheck = this.director.ThinkRoomTarget();

                if (roomIdNextCheck == RoomData.RoomID_Living || roomIdNextCheck == RoomData.RoomID_None )
                {
                    roomIdNextCheck = RoomData.RoomID_Entrance;
                }
                this.roomIdNext = roomIdNextCheck;

                if (this.roomIdNext == RoomData.RoomID_Entrance)
                {
                    if( Random.Range(0,2)==0)
                        this.kuma.Say(SpeechData.SpeechID_Bored);
                }
                else if (this.roomIdNext == RoomData.RoomID_Bedroom)
                {
                    this.kuma.Say(SpeechData.SpeechID_Spleepy);
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                        this.kuma.Say(SpeechData.SpeechID_Bored);
                }

                break;

            case Status.SitOut:
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);
                this.kuma.AnimationStart(this.kuma.animation_hash_sitout);
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


        switch (this.status)
        {
            case Status.In:
                this.kuma.Move(0f, Room.LineY_Kuma_ActiveLine, 1f);

                break;
            case Status.Wait:


                break;
            case Status.WalkToLeft:
                this.kuma.Move(-7, Room.LineY_Kuma_ActiveLine, 1f);
                //this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                break;
            case Status.WalkToRight:
                this.kuma.Move(7, Room.LineY_Kuma_ActiveLine , 1f);
                //this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);

                break;
        }
    }

    private void StatusUpdateFinished()
    {
        switch (this.status)
        {
            case Status.In:
                if (this.kuma.X <= 0f)
                {
                    this.StatusChange(Status.SitIn);
                }
                break;
            case Status.SitIn:
                if (this.kuma.GetIsAnimationFinished())
                {
                    this.StatusChange(Status.Sit);
                }
                break;

            case Status.SitOut:
                if (this.kuma.GetIsAnimationFinished())
                {
                    this.StatusChange(Status.WalkToRight);
                }
                break;

            case Status.Sit:
                if (this.statusTime.IsFinished && this.kuma.GetIsSaying())
                {
                    this.sayDelayTime.Start(1);
                }
                else if (this.statusTime.IsFinished && !this.kuma.GetIsSaying() && this.sayDelayTime.IsFinished )
                {
                    if (this.roomIdNext == RoomData.RoomID_None)
                    {
                        this.StatusChange(Status.Sit);
                    }
                    else
                    {
                        this.StatusChange(Status.SitOut);
                    }

                    /*
                    if (this.roomIdNext == RoomData.RoomID_None)
                    {
                        int roomIdNextCheck = this.director.ThinkRoomTarget();

                        if (roomIdNextCheck != RoomData.RoomID_Living)
                        {
                            this.roomIdNext = roomIdNextCheck;

                            if (this.roomIdNext == RoomData.RoomID_Entrance)
                            {
                                this.StatusChange(Status.SitOut);
                            }
                            else if (this.roomIdNext == RoomData.RoomID_Bedroom)
                            {
                                this.kuma.Say(SpeechData.SpeechID_Spleepy);
                                this.StatusChange(Status.SitOut);
                            }
                        }
                    }
                    else
                    {
                        this.StatusChange(Status.Sit);
                    }
                    */
                }
                break;

            case Status.Wait:

                if (this.statusTime.IsFinished)
                {
                    this.StatusChange(Status.WalkToLeft);
                }

                break;
            case Status.WalkToLeft:
                break;
            case Status.WalkToRight:

                if (this.kuma.X > Room.LineX_Kuma_OutLine_Right)
                {
                    this.director.MoveRoom(this.roomIdNext);
                    this.StatusChange(Status.Finish);
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

        this.backgroundRenderer.sprite = this.director.Resource.SpriteRoomBack_Living;

        this.StatusChange(Status.In);

    }

    public override void RoomDestroyed()
    {
        this.DeleteRoomObjectAll();   
    }


    public override void RoomUpdate()
    {
        this.StatusUpdate();
    }

    public override void RoomUpdateFinished()
    {
        this.StatusUpdateFinished();
    }


    private void GotoEntrance()
    {
        this.roomIdNext = RoomData.RoomID_Entrance;

        if( this.status == Status.Sit || this.status == Status.SitIn )
            this.StatusChange(Status.SitOut);
        else
            this.StatusChange(Status.WalkToRight);
    }

    private void GotoBedroom()
    {
        this.roomIdNext = RoomData.RoomID_Bedroom;
        if (this.status == Status.Sit || this.status == Status.SitIn)
            this.StatusChange(Status.SitOut);
        else
            this.StatusChange(Status.WalkToRight);
    }

    public override bool RunDebugCommand(string[] cmds, IDebugCommandListener commandListener)
    {
        if (cmds.Length >= 2 && cmds[0].Equals("goto"))
        {
            if (cmds[1].Equals("entrance"))
            {
                commandListener.Success(cmds);
                this.GotoEntrance();
            }
            else if (cmds[1].Equals("living"))
                commandListener.Error(cmds, "既に living にいます。");
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
        message += "Room Living\n";
        message += "\tStatus " + this.status.ToString() + "\n";
        message += "\tNext Room " + this.roomIdNext + "\n";



        switch (this.status)
        {
            case Status.In:
                break;
            case Status.Wait:
                break;
            case Status.WalkToLeft:
                break;
            case Status.WalkToRight:
                break;
            case Status.SitIn:
                break;
            case Status.Sit:
                message += "\tモーション時間  残り " + this.statusTime.activeTime + "秒 / " + this.statusTime.countTime+ "秒\n";
                break;
            case Status.SitOut:
                break;
        }

        commandListener.Message(message);
        return;
    }

}
