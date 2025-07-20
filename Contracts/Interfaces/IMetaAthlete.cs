namespace Contracts.Interfaces
{
    public interface IMetaAthlete
    {
        ICollection<IDetailedActivity> Activities { get; set; }
        int Id { get; set; }

        string ToJson();
        string ToString();
    }
}