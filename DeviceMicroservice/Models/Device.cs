using System;
using System.Collections.Generic;

namespace DeviceMicroservice.Models;

public partial class Device
{
    public int DeviceId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }

    public string? Stage { get; set; }
}
