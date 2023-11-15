using ExitGames.Client.Photon;
using Photon.Realtime;

public static class RoomPropertiesExtensions
{
    private const string StageKey = "StageID";
    private static readonly Hashtable propsToSet = new Hashtable();

    public static int getStageID(this Room room) {
        return (room.CustomProperties[StageKey] is int stageid) ? stageid : 0;
    }

    public static void setStageID(this Room room, int stageid) {
        propsToSet[StageKey] = stageid;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}
