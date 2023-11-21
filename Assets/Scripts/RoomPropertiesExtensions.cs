using ExitGames.Client.Photon;
using Photon.Realtime;

public static class RoomPropertiesExtensions
{
    private const string StageKey = "StageID";
    private const string TurnKey = "Turn";
    private static readonly Hashtable propsToSet = new Hashtable();

    public static int getStageID(this Room room) {
        return (room.CustomProperties[StageKey] is int stageid) ? stageid : 0;
    }
    public static int getTurn(this Room room) {
        return (room.CustomProperties[TurnKey] is int turn) ? turn : 0;
    }

    public static void setStageID(this Room room, int stageid) {
        propsToSet[StageKey] = stageid;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
    public static void setTurn(this Room room, int turn) {
        propsToSet[TurnKey] = turn;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}
