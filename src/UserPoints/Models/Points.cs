namespace UserPoints.Models;
/// <summary>
/// Model that represent the points that the a user have
/// </summary>
public class Points{
    /// <summary>
    /// The id of the user that owns the points
    /// </summary>
    public string? UserId{get; set;}
    /// <summary>
    /// Points of the user
    /// </summary>
    public int points {get; set;}
}