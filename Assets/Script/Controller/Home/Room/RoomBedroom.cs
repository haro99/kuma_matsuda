using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBedroom : Room
{
    public enum Status
    {
        In ,
        //Wait ,
        WalkToLeft , 
        WalkToRight ,
        BedIn ,
        BedActive ,
        BedOut ,
        Finish
    }
    private Status status;

    private LimitTimeCounter statusTime;

    private LimitTimeCounter sayCoolTime;

    private LimitTimeCounter sayDelayTime;

    private int roomIdNext;

    private static float LineX_Kuma_BedLine = -2.5f;




    public RoomBedroom()
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
                this.kuma.SetPosition(Room.LineX_Kuma_OutLine_Left, Room.LineY_Kuma_ActiveLine );
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                break;
            case Status.WalkToLeft:
                this.kuma.SetPosition(RoomBedroom.LineX_Kuma_BedLine, Room.LineY_Kuma_ActiveLine);
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                break;
            case Status.WalkToRight:
                this.kuma.AnimationStart(this.kuma.animation_hash_walk);
                break;

            case Status.BedIn:
                this.kuma.AnimationStart(this.kuma.animation_hash_bedin);
                break;

            case Status.BedActive:
                this.kuma.SetPosition(0, Room.LineY_Kuma_ActiveLine);
                this.kuma.AnimationStart(this.kuma.animation_hash_bedactive);

                this.statusTime.Start(Random.Range(60*60*2, 60*60*3));

                if (this.roomIdNext == RoomData.RoomID_None)
                {
                    this.roomIdNext = this.director.ThinkRoomTarget();

                    if (this.roomIdNext == RoomData.RoomID_None || this.roomIdNext == RoomData.RoomID_Bedroom)
                        this.roomIdNext = RoomData.RoomID_Entrance;
                }

                break;

            case Status.BedOut:
                this.kuma.AnimationStart(this.kuma.animation_hash_bedout);
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
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);

                break;
            case Status.WalkToLeft:
                this.kuma.Move(-7, Room.LineY_Kuma_ActiveLine, 1f);
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Left);
                break;
            case Status.WalkToRight:
                this.kuma.Move(7, Room.LineY_Kuma_ActiveLine , 1f);
                this.kuma.TurnTypeChange(KumaForRoom.TurnType.Right);

                break;

            case Status.BedIn:

                this.kuma.Move(0f, Room.LineY_Kuma_ActiveLine, 2f);

                break;

            case Status.BedOut:

                this.kuma.Move(RoomBedroom.LineX_Kuma_BedLine, Room.LineY_Kuma_ActiveLine, 2f);

                break;

            case Status.BedActive:
                if (!this.statusTime.IsFinished && !this.kuma.GetIsSaying())
                {
                    if( this.sayCoolTime.IsFinished )
                    {
                        this.kuma.Say(SpeechData.SpeechID_Zzz);
                        this.sayCoolTime.Start(3f);
                    }
                    else
                    {
                        this.sayCoolTime.Update();
                    }
                }
                break;


        }
    }

    private void StatusUpdateFinished()
    {
        switch (this.status)
        {
            case Status.In:

                if (this.kuma.X >= RoomBedroom.LineX_Kuma_BedLine)
                {
                    this.StatusChange(Status.BedIn);
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

            case Status.BedIn:

                if (this.kuma.GetIsAnimationFinished())
                    this.StatusChange(Status.BedActive);
                break;

            case Status.BedOut:

                if (this.kuma.GetIsAnimationFinished())
                    this.StatusChange(Status.WalkToLeft);
                break;

            case Status.BedActive:

                if (this.statusTime.IsFinished && this.kuma.GetIsSaying())
                {
                    this.sayDelayTime.Start(1f);
                }
                else if (this.statusTime.IsFinished && !this.kuma.GetIsSaying() && this.sayDelayTime.IsFinished )
                {
                    if (this.roomIdNext == RoomData.RoomID_None)
                    {
                        this.StatusChange(Status.BedActive);
                    }
                    else if (this.roomIdNext == RoomData.RoomID_Bedroom)
                    {
                        this.StatusChange(Status.BedActive);
                    }
                    else
                    {
                        this.StatusChange(Status.BedOut);
                    }
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
        this.sayCoolTime = new LimitTimeCounter();
        this.sayDelayTime = new LimitTimeCounter();


        this.backgroundRenderer.sprite = this.director.Resource.SpriteRoomBack_Bedroom;
        this.StatusChange(Status.In);



        GameObject bedFront = this.director.InstantiateRoomObject((GameObject)Resources.Load("prefabs/room/bedroom_front"), new Vector3(0, -1f, 0));
        this.AddRoomObject(bedFront);

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

        if( this.status == Status.BedIn || this.status == Status.BedActive )
            this.StatusChange(Status.BedOut);
        else
            this.StatusChange(Status.WalkToLeft);
    }

    private void GotoLiving()
    {
        this.roomIdNext = RoomData.RoomID_Living;

        if (this.status == Status.BedIn || this.status == Status.BedActive)
            this.StatusChange(Status.BedOut);
        else
            this.StatusChange(Status.WalkToLeft);
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
            {
                commandListener.Success(cmds);
                this.GotoLiving();
            }
            else if (cmds[1].Equals("bedroom"))
            {
                commandListener.Error(cmds, "既に bedroom にいます。");
            }

            return true;
        }


        return false;
    }


    public override void RunDebugStatus(IDebugCommandListener commandListener)
    {
        string message = "";
        message += "Room Bedroom\n";
        message += "\tStatus " + this.status.ToString() + "\n";
        message += "\tNext Room " + this.roomIdNext + "\n";


        switch (this.status)
        {
            case Status.In:
                break;
            case Status.WalkToLeft:
                break;
            case Status.WalkToRight:
                break;

            case Status.BedIn:
                break;

            case Status.BedActive:
                message += "\tモーション時間  残り " + this.statusTime.activeTime + "秒 / " + this.statusTime.countTime + "秒\n";
                break;

            case Status.BedOut:
                break;

        }

        commandListener.Message(message);
        return;
    }

}
