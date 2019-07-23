using System.Collections.Generic;

namespace Lif.ApiBasic.Interface
{
    public interface IApiResult
    {
        ApiState State { get; set; }
        string Msg { get; set; }
        Dictionary<string, string> Errors { get; set; }
    }
}
