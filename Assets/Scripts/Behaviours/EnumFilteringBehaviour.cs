using System.Linq;

public class EnumFilteringBehaviour : IEnumFiltering
{
    public string[] EnumToString(PatternEnum flags) => Filter(flags).Select(x => x.ToString()).ToArray();
    public PatternEnum[] Filter(PatternEnum flags) => System.Enum.GetValues(typeof(PatternEnum)).Cast<PatternEnum>().Where(x => flags.HasFlag(x) && x != 0).ToArray();
}
