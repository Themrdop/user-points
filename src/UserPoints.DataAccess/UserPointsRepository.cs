using MongoDB.Driver;
using UserPoints.Interfaces;
using UserPoints.Models;

namespace UserPoints.DataAccess 
{
    /// <summary>
    /// Implementation of the interface for the data repository
    /// </summary>
    public class UserPointsRepository : IUserPointsRepository
    {
        private readonly DataBaseImplementation<Points> _database;

        public UserPointsRepository(DataBaseImplementation<Points> _database)
        {
            this._database = _database;
        }

        /// <summary>
        /// Add the points to an user base on the user ID 
        /// </summary>
        /// <param name="points">The points that should be added and the user ID to whom it will be added<</param>
        /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
        /// <returns>The points structure with the sum of the points and the user ID who own them</returns>
        public async Task<Points> AddPoints(Points points, CancellationToken cancellationToken)
        {
            var dataBasePoints = await PointsByUser(points.UserId, cancellationToken);

            var sumOfPoints = dataBasePoints + points.points;

            var updateDef = Builders<Points>.Update.Set(o => o.points, sumOfPoints);

            await _database.Values.UpdateOneAsync(x=> x.UserId == points.UserId, updateDef, new UpdateOptions { IsUpsert = true}, cancellationToken);

            return new Points { points = sumOfPoints, UserId = points.UserId};
        }

        /// <summary>
        /// Returns the numer of ponts of a user
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
        /// <returns>The number of points that a user have</returns>
        public async Task<int> PointsByUser(string? userId, CancellationToken cancellationToken)
        {
            var filter = Builders<Points>.Filter.Eq(p => p.UserId, userId);

            var dataBasePoints = await _database.Values.FindAsync(filter, null, cancellationToken);

            var points = dataBasePoints.FirstOrDefault();

            if (points is null)
                return 0;

            return points.points;
        }

        /// <summary>
        /// Substract the amount of points that are sent as a parameter.
        /// </summary>
        /// <param name="points">The points that should be removed and the user ID to whom it will be remove</param>
        /// <param name="cancellationToken">The cancelation token in case that the cliente cancel the request</param>
        /// <returns>The points structure with the substract of the points and the user ID who own them</returns>
        public async Task<Points> RemovePoints(Points points, CancellationToken cancellationToken)
        {
            var dataBasePoints = await PointsByUser(points.UserId, cancellationToken);

            var sumOfPoints = dataBasePoints - points.points;

            var updateDef = Builders<Points>.Update.Set(o => o.points, sumOfPoints);

            await _database.Values.UpdateOneAsync(x => x.UserId == points.UserId, updateDef, null, cancellationToken);

            return new Points { points = sumOfPoints, UserId = points.UserId };
        }
    }
}
