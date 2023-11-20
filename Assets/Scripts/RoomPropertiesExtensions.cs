using ExitGames.Client.Photon;
using Photon.Realtime;

public static class RoomPropertiesExtensions
{
    private const string StageKey = "StageID";
    private const string StartFlagKey = "StartFlag";
    private const string StartWindowKey = "StartWindow";
    private static readonly Hashtable propsToSet = new Hashtable();

    public static int getStageID(this Room room) {
        return (room.CustomProperties[StageKey] is int stageid) ? stageid : 0;
    }
    public static int getStartFlag(this Room room) {
        return (room.CustomProperties[StartFlagKey] is int startflag) ? startflag : 0;
    }
    public static bool getStartWindow(this Room room) {
        return (room.CustomProperties[StartWindowKey] is bool startwindow) ? startwindow : false;
    }

    public static void setStageID(this Room room, int stageid) {
        propsToSet[StageKey] = stageid;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
    public static void setStartFlag(this Room room, int flag) {
        propsToSet[StartFlagKey] = flag;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
    public static void setStartWindow(this Room room, bool window) {
        propsToSet[StartWindowKey] = window;
        room.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}
