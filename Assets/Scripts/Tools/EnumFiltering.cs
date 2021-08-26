using System.Linq;

public static class EnumFiltering
{
    public static string[] EnumToString(BulletTypeEnum flags) => Filter(flags).Select(x => x.ToString()).ToArray();
    public static BulletTypeEnum[] Filter(BulletTypeEnum flags) => System.Enum.GetValues(typeof(BulletTypeEnum)).Cast<BulletTypeEnum>().Where(x => flags.HasFlag(x) && x != 0).ToArray();
}
