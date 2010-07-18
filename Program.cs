using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Input;
using SdlDotNet.Audio;

namespace AntiCulturePlanet
{
    /// <summary>
    /// Program's main controller
    /// </summary>
    internal class Program
    {
        #region Constants
        /// <summary>
        /// Screen width
        /// </summary>
        private const int screenWidth = 640;

        /// <summary>
        /// Screen height
        /// </summary>
        private const int screenHeight = 480;

        /// <summary>
        /// Target FPS
        /// </summary>
        private const int targetFps = 60;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Planet Generator
        /// </summary>
        private PlanetGenerator planetGenerator;

        /// <summary>
        /// Planet
        /// </summary>
        private Planet planet;

        /// <summary>
        /// Planet's view
        /// </summary>
        private PlanetViewer planetView;

        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random;
        #endregion

        #region Constructor
        public Program()
        {
            random = new Random();
            planetGenerator = new PlanetGenerator();
            planet = planetGenerator.Build(random);
            planetView = new LowResPlanetViewer(screenWidth, screenHeight);
        }
        #endregion

        #region Internal Methods
        internal void Start()
        {
            Events.TargetFps = targetFps;
            Events.Tick += Update;
            Events.KeyboardDown += OnKeyboardDown;
            Events.KeyboardUp += OnKeyboardUp;
            Events.MouseMotion += OnMouseMotion;
            Events.MouseButtonDown += OnMouseDown;
            Events.MouseButtonUp += OnMouseUp;
            Events.MusicFinished += OnMusicFinished;
            Events.Run();
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">run parameters</param>
        internal static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }
        #endregion
    }
}
