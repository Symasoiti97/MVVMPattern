using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Api.API
{
    public class ApiWorker<T> where T : class
    {

        private readonly IApiSetting _apiSetting;
        private readonly IApi<T> _api;

        public event Action<T> EventStart;
        public event Action EventAbort;

        public ApiWorker(IApi<T> api, IApiSetting apiSetting)
        {
            _api = api;
            _apiSetting = apiSetting;
        }

        public void Parse()
        {
            Worker();
        }

        private async Task Worker()
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                var reader = await webClient.DownloadStringTaskAsync(_apiSetting.Url);

                var list = _api.Parse(reader);

                EventStart?.Invoke(list);

                EventAbort?.Invoke();
            }
        }
    }
}
