using UserPoints.Models;

namespace UserPoints.Interfaces
{
    /// <summary>
    /// Interface for the data repository
    /// </summary>
    public interface IUserPointsRepository
    {
        /// <summary>
        /// Add the points to an user base on the user ID 
        /// </summary>
        /// <param name="points">The points that should be added and the user ID to whom it will be added</param>
        /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
        /// <returns>Return the user resulting sum of points with the user ID</returns>
        Task<Points> AddPoints(Points points, CancellationToken cancellationToken);
        /// <summary>
        /// Substract the amount of points that are sent as a parameter.
        /// </summary>
        /// <param name="points">The points that should ve substracted to the user and the user Id to who they should be remove</param>
        /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
        /// <returns>Return the user resulting substract of points with the user ID</returns>
        Task<Points> RemovePoints(Points points, CancellationToken cancellationToken);
        /// <summary>
        /// Returns the numer of ponts of a user
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
        /// <returns>Return the users points</returns>
        Task<int> PointsByUser(string userId, CancellationToken cancellationToken);
    }
}
