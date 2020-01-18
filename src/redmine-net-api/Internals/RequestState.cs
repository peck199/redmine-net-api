#if NET20

using System.IO;
using System.Net;
using System.Text;

namespace Redmine.Net.Api.Internals
{
    /// <summary>
    /// The RequestState class passes data across async calls.
    /// </summary>
    public class RequestState  
    {
        private const int BUFFER_SIZE = 1024;

        /// <summary>
        /// 
        /// </summary>
        public Stream ResponseStream { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decoder StreamDecode { get; set; } = Encoding.UTF8.GetDecoder();
        
        /// <summary>
        /// 
        /// </summary>
        public WebRequest Request { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] BufferRead { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public StringBuilder RequestData { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RequestState()  
        {  
            BufferRead = new byte[BUFFER_SIZE];  
            RequestData = new StringBuilder(string.Empty);  
            Request = null;  
            ResponseStream = null;  
        }       
    }
}

#endif