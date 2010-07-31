using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// For physics and movements
    /// </summary>
    static class Physics
    {
        /// <summary>
        /// Try make walk
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="speed">speed</param>
        /// <param name="planet">planet</param>
        /// <param name="timeDelta">time delta</param>
        internal static void TryMakeWalk(AbstractEntity entity, double speed, Planet planet, double timeDelta)
        {
            TryMakeWalk(entity, speed, 0, planet, timeDelta);
        }

        /// <summary>
        /// Try make walk
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="speed">speed</param>
        /// <param name="angleOffsetRadian">angle offset (radian) (default: 0)</param>
        /// <param name="planet">planet</param>
        /// <param name="timeDelta">time delta</param>
        internal static void TryMakeWalk(AbstractEntity entity, double speed, double angleOffsetRadian, Planet planet, double timeDelta)
        {
            double xMove = Math.Cos(entity.AngleRadian + angleOffsetRadian) * speed * timeDelta * 0.1;
            double yMove = Math.Sin(entity.AngleRadian + angleOffsetRadian) * speed * timeDelta * 0.1;

            double newX = entity.X + xMove;
            double newY = entity.Y + yMove;

            while (newX >= planet.Width)
                newX -= planet.Width;

            while (newY >= planet.Height)
                newY -= planet.Height;

            while (newX < 0)
                newX += planet.Width;

            while (newY < 0)
                newY += planet.Height;

            entity.Move(newX, newY);
        }
    }
}
