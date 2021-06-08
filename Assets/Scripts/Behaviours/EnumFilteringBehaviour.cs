using System.Linq;

public class EnumFilteringBehaviour : IEnumFiltering
{
    public string[] EnumToString(BulletTypeEnum flags) => Filter(flags).Select(x => x.ToString()).ToArray();
    public BulletTypeEnum[] Filter(BulletTypeEnum flags) => System.Enum.GetValues(typeof(BulletTypeEnum)).Cast<BulletTypeEnum>().Where(x => flags.HasFlag(x) && x != 0).ToArray();
}
