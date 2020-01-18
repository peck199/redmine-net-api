#if NET20

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Redmine.Net.Api.Internals
{
    internal sealed partial class RestClient : IAsyncRestClient
    {

        public readonly ManualResetEvent AllDone = new ManualResetEvent(false);
        private const int BUFFER_SIZE = 1024;


        public IAsyncResult Create()
        {
            Uri httpSite = new Uri("");
            WebRequest wreq = WebRequest.Create(httpSite);

            RequestState rs = new RequestState
            {
                Request = wreq
            };


            IAsyncResult r = wreq.BeginGetResponse(RespCallback, rs);

            // Wait until the ManualResetEvent is set so that the application   
            // does not exit until after the callback is called.  
            AllDone.WaitOne();

            return r;
        }

        private void RespCallback(IAsyncResult ar)
        {
            RequestState rs = (RequestState)ar.AsyncState;

            WebRequest req = rs.Request;

            WebResponse resp = req.EndGetResponse(ar);

            Stream responseStream = resp.GetResponseStream();

            rs.ResponseStream = responseStream;

            responseStream.BeginRead(rs.BufferRead, 0, BUFFER_SIZE, ReadCallBack, rs);
        }

        private void ReadCallBack(IAsyncResult asyncResult)
        {
            RequestState rs = (RequestState)asyncResult.AsyncState;

            Stream responseStream = rs.ResponseStream;

            int read = responseStream.EndRead(asyncResult);
            if (read > 0)
            {
                char[] charBuffer = new char[BUFFER_SIZE];

                int len = rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);

                rs.RequestData.Append(Encoding.UTF8.GetString(rs.BufferRead, 0, read));

                // Continue reading data until responseStream.EndRead returns –1.  
                responseStream.BeginRead(rs.BufferRead, 0, BUFFER_SIZE, ReadCallBack, rs);
            }
            else
            {
                if (rs.RequestData.Length > 0)
                {
                    string strContent = rs.RequestData.ToString();
                    //TODO: do something with strContent
                }
                responseStream.Close();
                // Set the ManualResetEvent so the main thread can exit.  
                AllDone.Set();
            }
        }
    }
}

#endif