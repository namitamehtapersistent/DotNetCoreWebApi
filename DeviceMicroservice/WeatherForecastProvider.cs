using DeviceMicroservice.Models;

namespace DeviceMicroservice;

    public interface IDeviceProvider
    {
        Device[] GetDevice();
    }
    public class DeviceProvider : IDeviceProvider
    {
       private readonly IDataAccess dataAccess;

    public DeviceProvider(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    public Device[] GetDevice() => dataAccess.Get().ToArray();
     
    }

