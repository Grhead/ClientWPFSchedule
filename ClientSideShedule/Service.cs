using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;
using Google.Protobuf.Reflection;
using ApiService;

namespace ClientSideShedule
{
    class Service
    {
        public static Frame? frame;
        public static GrpcChannel channelRPC = GrpcChannel.ForAddress("http://127.0.0.1:8787");
        public static SheduleService.SheduleServiceClient Client = new SheduleService.SheduleServiceClient(channelRPC);
    }
}
