using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Represents a spatial hash's bucket
    /// </summary>
    class Bucket : ICollection<AbstractEntity>
    {
        #region Fields and Parts
        /// <summary>
        /// Internal list of entities
        /// </summary>
        private HashSet<AbstractEntity> internalList;

        /// <summary>
        /// Whether bucket needs to be redrawn
        /// </summary>
        private bool isNeedRedraw;
        #endregion

        #region Constructor
        /// <summary>
        /// Create bucket
        /// </summary>
        public Bucket()
        {
            isNeedRedraw = true;
            internalList = new HashSet<AbstractEntity>();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Refresh the list of entities to update from this bucket
        /// </summary>
        /// <param name="listEntityToUpdate">list of entities to update</param>
        internal void RefreshListEntityToUpdate(List<AbstractEntity> listEntityToUpdate)
        {
            listEntityToUpdate.Clear();
            listEntityToUpdate.AddRange(internalList);
        }
        #endregion

        #region ICollection<AbstractEntity> Members
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="item">entity</param>
        public void Add(AbstractEntity item)
        {
            isNeedRedraw = true;
            internalList.Add(item);
        }

        /// <summary>
        /// Clear entities from bucket
        /// </summary>
        public void Clear()
        {
            isNeedRedraw = true;
            internalList.Clear();
        }

        /// <summary>
        /// Whether bucket contains entity
        /// </summary>
        /// <param name="item">entity</param>
        /// <returns>Whether bucket contains entity</returns>
        public bool Contains(AbstractEntity item)
        {
            return internalList.Contains(item);
        }

        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(AbstractEntity[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// How many entities
        /// </summary>
        public int Count
        {
            get { return internalList.Count; }
        }

        /// <summary>
        /// Not read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="item">entity</param>
        /// <returns>whether could remove</returns>
        public bool Remove(AbstractEntity item)
        {
            isNeedRedraw = true;
            return internalList.Remove(item);
        }

        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<AbstractEntity> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether bucket needs to be redrawn
        /// </summary>
        public bool IsNeedRedraw
        {
            get { return isNeedRedraw; }
            set { isNeedRedraw = value; }
        }
        #endregion
    }
}
