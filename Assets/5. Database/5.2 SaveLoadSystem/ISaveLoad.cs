using System.Threading.Tasks;

public interface ISaveLoad
{
    public void Save();
    public Task LoadAsync();
    public DataType GetDataType();
}
