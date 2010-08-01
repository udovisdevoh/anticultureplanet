using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Sprite manager
    /// </summary>
    internal static class SpriteManager
    {
        #region Fields and parts
        /// <summary>
        /// Sprite list
        /// </summary>
        private static Dictionary<Type, EntitySprite> spriteList = new Dictionary<Type, EntitySprite>();
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get sprite for entity type
        /// </summary>
        /// <param name="type">entity type</param>
        /// <param name="isRotate">whether the entity can rotate</param>
        /// <returns>sprite for entity type</returns>
        internal static EntitySprite GetSprite(Type entityType, bool isRotate)
        {
            EntitySprite sprite;
            if (!spriteList.TryGetValue(entityType, out sprite))
            {
                sprite = BuildSprite(entityType, isRotate);
                spriteList.Add(entityType, sprite);
            }

            return sprite;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build sprite from entity type
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <param name="isRotate">whether the entity can rotate</param>
        /// <returns>new sprite for entity</returns>
        private static EntitySprite BuildSprite(Type entityType, bool isRotate)
        {
            return BuildSprite(entityType, 0, isRotate);
        }

        /// <summary>
        /// Build sprite from entity type
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <param name="spriteVariationIndex">sprite variation index (default: 0)</param>
        /// <param name="isRotate">whether the entity can rotate</param>
        /// <returns>new sprite for entity</returns>
        private static EntitySprite BuildSprite(Type entityType, int spriteVariationIndex, bool isRotate)
        {
            return new EntitySprite(entityType.Name + spriteVariationIndex + ".png",isRotate);
        }
        #endregion
    }
}
