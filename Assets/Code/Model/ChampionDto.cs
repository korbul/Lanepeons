using System.Collections.Generic;
/// <summary>
/// This object contains champion data.
/// </summary>
[System.Serializable]
public class ChampionDto
{
    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public ImageDto Image { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    public string Name { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    //public IEnumerable<SkinDto> Skins { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public string Title { get; set; }
}