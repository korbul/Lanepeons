using System.Collections.Generic;
/// <summary>
/// This object contains champion list data.
/// </summary>
[System.Serializable]
public class ChampionListDto
{
    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public ChampionDto[] Data { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// No description available from Riot Documentation
    /// </summary>
    public string Version { get; set; }
}