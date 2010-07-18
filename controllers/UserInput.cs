using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Which keys are up or down
    /// </summary>
    class UserInput
    {
        #region Fields
        private bool isPressUp = false;

        private bool isPressDown = false;

        private bool isPressLeft = false;

        private bool isPressRight = false;

        private bool isPressShift = false;

        private bool isPressSpace = false;

        private bool isPressMouseButtonLeft = false;

        private bool isPressMouseButtonRight = false;

        private bool isPressMouseButtonCenter = false;

        private short currentMouseRelativeX = 0;

        private short currentMouseRelativeY = 0;
        #endregion

        #region Properties
        public bool IsPressUp
        {
            get { return isPressUp; }
            set { isPressUp = value; }
        }

        public bool IsPressDown
        {
            get { return isPressDown; }
            set { isPressDown = value; }
        }

        public bool IsPressLeft
        {
            get { return isPressLeft; }
            set { isPressLeft = value; }
        }

        public bool IsPressRight
        {
            get { return isPressRight; }
            set { isPressRight = value; }
        }

        public bool IsPressShift
        {
            get { return isPressShift; }
            set { isPressShift = value; }
        }

        public bool IsPressSpace
        {
            get { return isPressSpace; }
            set { isPressSpace = value; }
        }

        public bool IsPressMouseButtonLeft
        {
            get { return isPressMouseButtonLeft; }
            set { isPressMouseButtonLeft = value; }
        }

        public bool IsPressMouseButtonRight
        {
            get { return isPressMouseButtonRight; }
            set { isPressMouseButtonRight = value; }
        }

        public bool IsPressMouseButtonCenter
        {
            get { return isPressMouseButtonCenter; }
            set { isPressMouseButtonCenter = value; }
        }

        public short MouseMotionX
        {
            get { return currentMouseRelativeX; }
            set { currentMouseRelativeX = value; }
        }

        public short MouseMotionY
        {
            get { return currentMouseRelativeY; }
            set { currentMouseRelativeY = value; }
        }
        #endregion
    }
}
