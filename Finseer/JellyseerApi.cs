using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Finseer;

/// <summary>
/// api manager for jellyseer.
/// </summary>
public sealed class JellyseerApi : IDisposable
{
    private HttpClient _client;

    private JellyseerApi(string basePath, string apikey)
    {
        _client = NewSeerClient(basePath, apikey);
    }

    private static HttpClient NewSeerClient(string basePath, string apikey)
    {
        basePath = basePath.TrimEnd('/');
        var client = new HttpClient { BaseAddress = new Uri(basePath), Timeout = TimeSpan.FromSeconds(30), };
        client.DefaultRequestHeaders.Add("X-Api-Key", apikey);
        return client;
    }

    /// <summary>
    /// Initilizes a new jellyseer instance and tests your settings.
    /// </summary>
    /// <param name="basePath">basePath for jellyseer.</param>
    /// <param name="apikey">apikey for jellyseer.</param>
    /// <returns>JellyseerApi.</returns>
    public static async Task<JellyseerApi> Create(string basePath, string apikey)
    {
        try
        {
            var inst = new JellyseerApi(basePath, apikey);
            await TestConnectionInternal(inst._client).ConfigureAwait(false);
            return inst;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// tests jellyseer settings.
    /// </summary>
    /// <returns>Void.</returns>
    private static async Task TestConnectionInternal(HttpClient client)
    {
        var res = await client.GetAsync("/settings/main").ConfigureAwait(false);
        res.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// test connection intende to be used in the configureation ui for the user to quickly test settings.
    /// </summary>
    /// <param name="url">basePath for jellyseer.</param>
    /// <param name="apikey">apikey for jellyseer.</param>
    /// <returns>bool.</returns>
    public static async Task<bool> TestConnection(string url, string apikey)
    {
        using var cli = NewSeerClient(url, apikey);
        try
        {
            await TestConnectionInternal(cli).ConfigureAwait(false);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    /// <summary>
    /// Cleanup httpclient resource.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }
}
