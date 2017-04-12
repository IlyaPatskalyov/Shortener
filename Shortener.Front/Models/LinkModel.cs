using System;
using System.Runtime.Serialization;

namespace Shortener.Front.Models
{
    [DataContract]
    public class LinkModel
    {
        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "countOfRedirects")]
        public int CountOfRedirects { get; set; }
    }
}