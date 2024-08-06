using DeviceMicroservice.Models;

namespace DeviceMicroservice;

public interface IDeviceCreator
{
    public Device Create(Device wf);

    public Device Update(int temperatureC, Device wf);

    public Device Delete(int temperatureC);
}

public class DeviceCreator : IDeviceCreator
{
    private readonly IDataAccess dataAccess;
     
      public DeviceCreator(IDataAccess dataAccess)
      {
        this.dataAccess = dataAccess;
      }

    public Device Create(Device wf)
    {
        dataAccess.Create(wf);
        return wf;
    }

    public Device Update(int temperatureC, Device wf)
    {
        dataAccess.Update(temperatureC, wf);
        return wf;
    }

    public Device Delete(int temperatureC)
    {
        return dataAccess.Delete(temperatureC);
    }
}

