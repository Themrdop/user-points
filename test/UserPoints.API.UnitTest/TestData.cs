using UserPoints.Models;

namespace UserPoints.DataAccess
{
    public static class TestData
    {
        public static Points TestPoints
        {
            get
            {
                return new Points
                {
                    points = 10,
                    UserId = "steph"
                };
            }
        }
    }
}
