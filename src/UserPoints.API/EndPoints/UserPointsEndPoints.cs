public static class UserPointsEndPoints{
    /// <summary>
    /// this method map the end points to the corresponding method
    /// </summary>
    /// <param name="app">Web application object that will map the endpoints</param>
    public static void ConfigureApi(this WebApplication app){
        app.MapPut("/AddPoints",AddPointsAsync);
        app.MapPut("/RemovePoints",RemovePointsAsync);
        app.MapGet("/PointsByUser",PointsByUserAsync);
    }

    /// <summary>
    /// Add the points to an user base on the user ID 
    /// </summary>
    /// <param name="points">The points that should be added and the user ID to whom it will be added</param>
    /// <param name="userPoints">The representation of the data reporsitory, this will be passed by the DI container</param>
    /// <param name="logger">The logger component, this will be passed by the DI container</param>
    /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
    /// <returns>The that contains the response of the services</returns>
    internal static async Task<IResult> AddPointsAsync(Points points, 
                                                      IUserPointsRepository userPoints,
                                                      ILogger<Points> logger,
                                                      CancellationToken cancellationToken){
        if (InputAreNotValid(points))
            return Results.BadRequest();

        try
        {
            var response = await userPoints.AddPoints(points, cancellationToken);

            logger?.LogInformation("Points {points} added to {UserId}", points.points, points.UserId);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            logger?.LogError("Error adding points, exception: {ex}", ex);
            return Results.Problem(ex.Message);
        }
    }
    /// <summary>
    /// Substract the amount of points that are sent as a parameter.
    /// </summary>
    /// <param name="points">The points that should be removed and the user ID to whom it will be remove</param>
    /// <param name="userPoints">The representation of the data reporsitory, this will be passed by the DI container</param>
    /// <param name="logger">The logger component, this will be passed by the DI container</param>
    /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
    /// <returns>The result that contains the response of the services</returns>
    internal static async Task<IResult> RemovePointsAsync(Points points, 
                                                         IUserPointsRepository userPoints,
                                                         ILogger<Points> logger,
                                                         CancellationToken cancellationToken)
    {
        if (InputAreNotValid(points))
            return Results.BadRequest();

        try
        {
            var response = await userPoints.RemovePoints(points, cancellationToken);

            logger?.LogInformation("Points {points} added to {UserId}", points.points, points.UserId);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            logger?.LogError("Error removing points, exception: {ex}", ex);
            return Results.Problem(ex.Message);
        }
    }
    /// <summary>
    /// Returns the numer of ponts of a user
    /// </summary>
    /// <param name="userId">The user Id</param>
    /// <param name="userPoints">The representation of the data reporsitory, this will be passed by the DI container</param>
    /// <param name="logger">The logger component, this will be passed by the DI container</param>
    /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
    /// <returns>The result that contains the response of the services</returns>
    internal static async Task<IResult> PointsByUserAsync(string userId, 
                                                         IUserPointsRepository userPoints,
                                                         ILogger<Points> logger,
                                                         CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId) | string.IsNullOrWhiteSpace(userId))
            return Results.BadRequest();

        try
        {
            var response = await userPoints.PointsByUser(userId, cancellationToken);

            logger?.LogInformation("Points query for {UserId}", userId);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            logger?.LogError("Error cosulting points, exception: {ex}", ex);

            return Results.Problem(ex.Message);
        }
    }

    /// <summary>
    /// Helper method that validates if there're no invalid values
    /// </summary>
    /// <param name="points">The data struncture that will be validated</param>
    /// <returns>True if the intup is valid, false otherwise</returns>
    private static bool InputAreNotValid(Points points)
    {
        return points is null || points.UserId is null || points.points < 0;
    }
}