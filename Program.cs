﻿using System;
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

        /// <summary>
        /// Full screen or not
        /// </summary>
        private const bool isFullScreen = true;
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

        /// <summary>
        /// Previous date time (for time delta)
        /// </summary>
        private DateTime previousDateTime = DateTime.Now;

        /// <summary>
        /// Main drawing surface
        /// </summary>
        private Surface mainSurface;
        #endregion

        #region Constructor
        public Program()
        {
            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, false, false, isFullScreen, true);
            random = new Random();
            planetGenerator = new PlanetGenerator();
            planet = planetGenerator.Build(random);
            planetView = new LowResPlanetViewer(mainSurface, screenWidth, screenHeight);
        }
        #endregion

        #region Internal Methods
        internal void Start()
        {
            Events.TargetFps = targetFps;
            Events.Tick += Update;
            Events.KeyboardDown += OnKeyboardDown;
            Events.KeyboardUp += OnKeyboardUp;
            /*Events.MouseMotion += OnMouseMotion;
            Events.MouseButtonDown += OnMouseDown;
            Events.MouseButtonUp += OnMouseUp;
            Events.MusicFinished += OnMusicFinished;*/
            Events.Run();
        }
        #endregion

        #region Event Handlers
        public void Update(object sender, TickEventArgs args)
        {
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 16.0;
            previousDateTime = DateTime.Now;
        }

        public void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            /*if (args.Key == Key.UpArrow || args.Key == Key.W)
                userInput.IsPressUp = true;
            else if (args.Key == Key.DownArrow || args.Key == Key.S)
                userInput.IsPressDown = true;
            else if (args.Key == Key.LeftArrow || args.Key == Key.A)
                userInput.IsPressLeft = true;
            else if (args.Key == Key.RightArrow || args.Key == Key.D)
                userInput.IsPressRight = true;
            else if (args.Key == Key.LeftShift || args.Key == Key.C)
                userInput.IsPressCrouch = true;
            else if (args.Key == Key.Space)
                userInput.IsPressJump = true;
            else if (args.Key == Key.Tab)
                gameViewer.IsMiniMapOn = !gameViewer.IsMiniMapOn;*/
        }

        public void OnKeyboardUp(object sender, KeyboardEventArgs args)
        {
            /*if (args.Key == Key.UpArrow || args.Key == Key.W)
                userInput.IsPressUp = false;
            else if (args.Key == Key.DownArrow || args.Key == Key.S)
                userInput.IsPressDown = false;
            else if (args.Key == Key.LeftArrow || args.Key == Key.A)
                userInput.IsPressLeft = false;
            else if (args.Key == Key.RightArrow || args.Key == Key.D)
                userInput.IsPressRight = false;
            else if (args.Key == Key.LeftShift || args.Key == Key.C)
                userInput.IsPressCrouch = false;
            else if (args.Key == Key.Space)
                userInput.IsPressJump = false;
            else */if (args.Key == Key.Escape)
                Events.QuitApplication();
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
