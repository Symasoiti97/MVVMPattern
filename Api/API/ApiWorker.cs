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
        private int _millisec = 30000;

        private readonly IApiSetting _apiSetting;
        private readonly IApi<T> _api;

        private CancellationTokenSource token;

        private bool _isActive = false;

        public event Action<T> EventStart;
        public event Action EventAbort;

        public ApiWorker(IApi<T> api, IApiSetting apiSetting)
        {
            _api = api;
            _apiSetting = apiSetting;
        }

        public void Start()
        {
            _isActive = true;
            token = new CancellationTokenSource();
            Worker(token.Token);
        }

        public void Start(int millisec)
        {
            _isActive = true;
            _millisec = millisec;
            token = new CancellationTokenSource();
            Worker(token.Token);
        }

        public void Abort()
        {
            _isActive = false;
            token?.Cancel();
        }

        private async Task Worker(CancellationToken token)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                while (_isActive)
                {
                    var reader = await webClient.DownloadStringTaskAsync(_apiSetting.Url);

                    var list = _api.Parse(reader);

                    EventStart?.Invoke(list);

                    try
                    {
                        await Task.Delay(_millisec, token);
                    }
                    catch (OperationCanceledException ex)
                    {
                    }
                }
            }

            EventAbort?.Invoke();
        }
    }
}
