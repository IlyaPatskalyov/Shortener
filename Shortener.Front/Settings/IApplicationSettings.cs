using Shortener.Front.Models;
using Shortener.Storage.EF;

namespace Shortener.Front.Settings
{
    public interface IApplicationSettings : IDbSettings
    {
        string LogPath { get; }
        FrontendMode FrontendMode { get; }
    }
}