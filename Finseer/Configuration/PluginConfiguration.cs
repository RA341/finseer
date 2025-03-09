using MediaBrowser.Model.Plugins;

namespace Finseer.Configuration;

/// <summary>
/// The configuration options.
/// </summary>
public enum SomeOptions
{
    /// <summary>
    /// Option one.
    /// </summary>
    OneOption,

    /// <summary>
    /// Second option.
    /// </summary>
    AnotherOption
}

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        // set default options here
        Apikey = string.Empty;
        Baseurl = "http://localhost:5055";
    }

    /// <summary>
    /// Gets or sets jellyseer apikey.
    /// </summary>
    public string Apikey { get; set; }

    /// <summary>
    /// Gets or sets jellyseer baseurl.
    /// </summary>
    public string Baseurl { get; set; }
}
