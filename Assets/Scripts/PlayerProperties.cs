using ExitGames.Client.Photon;
using Photon.Realtime;

public static class PlayerPropertiesExtensions
{
    private const string StageKey = "StageID";
    private static readonly Hashtable propsToSet = new Hashtable();

    public static int getStageID(this Player player) {
        return (player.CustomProperties[StageKey] is int stageid) ? stageid : 0;
    }

    public static void setStageID(this Player player, int stageid) {
        propsToSet[StageKey] = stageid;
        player.SetCustomProperties(propsToSet);
        propsToSet.Clear();
    }
}
