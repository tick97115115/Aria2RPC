using Aria2NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria2RPC.Services
{
    public interface IAria2RPCService
    {
        public Aria2NetClient GetClient();
        public Task RunAria2ServiceAsync();
        public Task SoftStopAsync();
        public void SoftStop();
        public void ForceShutdown();
    }
}
