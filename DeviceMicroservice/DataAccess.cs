using DeviceMicroservice.Models;

namespace DeviceMicroservice;
public interface IDataAccess
{
    public IList<Device> Get();

    public void Create(Device wf);

    public void Update(int temperatureC, Device wf);

    public Device Delete(int temperatureC);
}

public class DataAccess : IDataAccess
{
    private readonly DeviceManagementContext _context;

    public DataAccess()
    {
        this._context = new DeviceManagementContext();
    }

    public IList<Device> Get() => this._context.Devices.ToList();

    public void Create(Device wf)
    {
        this._context.Devices.Add(wf);
        this._context.SaveChanges();
    }

    public void Update(int temperatureC, Device wf)
    {
        var deviceToUpdate = this._context.Devices.FirstOrDefault(o => o.DeviceId == temperatureC);
        if (deviceToUpdate != null)
        {
            this._context.Entry(deviceToUpdate).CurrentValues.SetValues(wf);
            this._context.SaveChanges();
        }
    }

    public Device Delete(int temperatureC)
    {
        var weather = this._context.Devices.FirstOrDefault(x => x.DeviceId == temperatureC);
        if (weather != null)
        {
            this._context.Devices.Remove(weather);
            this._context.SaveChanges();
            return weather;
        }
        return null;

    }

}

