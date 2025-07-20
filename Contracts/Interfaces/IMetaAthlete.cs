namespace Contracts.Interfaces
{
    public interface IMetaAthlete
    {
        int Id { get; set; }

        string ToJson();
        string ToString();
    }
}