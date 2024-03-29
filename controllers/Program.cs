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
        private const int screenWidth = 800;

        /// <summary>
        /// Screen height
        /// </summary>
        private const int screenHeight = 600;

        /// <summary>
        /// Target FPS
        /// </summary>
        private const int targetFps = 60;

        /// <summary>
        /// Full screen or not
        /// </summary>
        private const bool isFullScreen = false;

        /// <summary>
        /// Planet width
        /// </summary>
        private const int planetWidth = 500;

        /// <summary>
        /// Planet height
        /// </summary>
        private const int planetHeight = 500;

        /// <summary>
        /// 1: normal, 2: double speed, 0.5: half the speed
        /// </summary>
        public const double SpeedMultiplier = 10.0;

        /// <summary>
        /// Max time we try to find a random place on map with some criteria
        /// </summary>
        public const int MaxTryFindRandomTilePosition = 10;

        /// <summary>
        /// How many seconds before mother nature refreshes global decay
        /// </summary>
        public const double EntityTransformationRefreshTime = 1.0;

        /// <summary>
        /// Bits per pixel
        /// </summary>
        public const int BitsPerPixel = 16;

        /// <summary>
        /// Spatial hash's bucket size
        /// </summary>
        public const int BucketSize = 5;
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
        /// Mother nature
        /// </summary>
        private MotherNature motherNature;

        /// <summary>
        /// Planet's view
        /// </summary>
        private PlanetViewer planetViewer;

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

        /// <summary>
        /// User input;
        /// </summary>
        private UserInput userInput;

        /// <summary>
        /// List of entities to update
        /// </summary>
        private List<AbstractEntity> listEntityToUpdate;

        /// <summary>
        /// List of animals to update
        /// </summary>
        private HashSet<AbstractAnimalEntity> listAnimalToUpdate;
        #endregion

        #region Constructor
        public Program()
        {
            listEntityToUpdate = new List<AbstractEntity>();
            listAnimalToUpdate = new HashSet<AbstractAnimalEntity>();
            userInput = new UserInput();
            mainSurface = Video.SetVideoMode(screenWidth, screenHeight, BitsPerPixel, false, false, isFullScreen, true, true);

            random = new Random();
            motherNature = new MotherNature();
            planetGenerator = new PlanetGenerator(random);
            planetGenerator.Width = planetWidth;
            planetGenerator.Height = planetHeight;
            planet = planetGenerator.Build();
            planetViewer = new PlanetViewerLowRes(mainSurface, screenWidth, screenHeight, planet);
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
        /// <summary>
        /// Update program
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">arguments</param>
        internal void Update(object sender, TickEventArgs args)
        {
            DateTime currentTime = DateTime.Now;
            double timeDelta = ((TimeSpan)(DateTime.Now - previousDateTime)).TotalMilliseconds / 16.0;
            previousDateTime = currentTime;

            //We parse user input
            if (userInput.IsPressLeft)
                planetViewer.MoveView(-1, 0, planet.Width, planet.Height);
            if (userInput.IsPressRight)
                planetViewer.MoveView(1, 0, planet.Width, planet.Height);
            if (userInput.IsPressUp)
                planetViewer.MoveView(0, -1, planet.Width, planet.Height);
            if (userInput.IsPressDown)
                planetViewer.MoveView(0, 1, planet.Width, planet.Height);

            motherNature.UpdatePopulationRegualtor(planet, currentTime);

            //We get a list of random entities to update
            planet.EntityCollection.GetRandomBucket(random).RefreshListEntityToUpdate(listEntityToUpdate);
            foreach (AbstractEntity entity in listEntityToUpdate)
            {
                //For plants
                if (entity is AbstractPlantEntity)
                {
                    AbstractPlantEntity plant = (AbstractPlantEntity)entity;
                    plant.TryReproduce(planet, currentTime);
                }

                //Decay
                if (entity.DecayTime > 0)
                {
                    TimeSpan timeSpanSinceCreation = (TimeSpan)(currentTime - entity.CreationTime);
                    if (timeSpanSinceCreation.TotalSeconds * Program.SpeedMultiplier > entity.DecayTime || entity.Integrity <= 0)
                    {
                        if (entity is AbstractPlantEntity)
                        {
                            ((AbstractPlantEntity)(entity)).GoToNextPhaseOrDecay(planet);
                        }
                        else
                        {
                            entity.Decay(planet);
                        }
                    }
                }
            }

            //For animals
            listAnimalToUpdate.Clear();
            listAnimalToUpdate.UnionWith(planet.EntityCollection.AnimalList);
            foreach (AbstractAnimalEntity animal in listAnimalToUpdate)
            {
                if (random.Next(0, 10) == 0)
                {
                    if (!animal.TryReproduce(planet, currentTime))
                    {
                        if (!animal.TryGrow(planet))
                        {
                            AbstractEntity nearestPrey;
                            animal.TryMakeWalkFightOrFlight(planet, random, timeDelta, out nearestPrey);
                            if (nearestPrey != null)
                                animal.TryEat(nearestPrey, planet);
                        }
                    }

                    animal.FoodReserve -= ((animal.Size / 250.0) * Program.SpeedMultiplier / 10.0);
                    if (animal.FoodReserve <= 0 ||
                        animal.Integrity <= 0 ||
                        planet.EntityCollection.IsDetectCollision(animal, planet))
                        animal.Decay(planet);
                }
            }

            planetViewer.Update(planet);

            mainSurface.Update();
        }

        internal void OnKeyboardDown(object sender, KeyboardEventArgs args)
        {
            if (args.Key == Key.UpArrow || args.Key == Key.W)
                userInput.IsPressUp = true;
            else if (args.Key == Key.DownArrow || args.Key == Key.S)
                userInput.IsPressDown = true;
            else if (args.Key == Key.LeftArrow || args.Key == Key.A)
                userInput.IsPressLeft = true;
            else if (args.Key == Key.RightArrow || args.Key == Key.D)
                userInput.IsPressRight = true;
            else if (args.Key == Key.LeftShift || args.Key == Key.C)
                userInput.IsPressShift = true;
            else if (args.Key == Key.Space)
                userInput.IsPressSpace = true;
        }

        internal void OnKeyboardUp(object sender, KeyboardEventArgs args)
        {
            if (args.Key == Key.UpArrow || args.Key == Key.W)
                userInput.IsPressUp = false;
            else if (args.Key == Key.DownArrow || args.Key == Key.S)
                userInput.IsPressDown = false;
            else if (args.Key == Key.LeftArrow || args.Key == Key.A)
                userInput.IsPressLeft = false;
            else if (args.Key == Key.RightArrow || args.Key == Key.D)
                userInput.IsPressRight = false;
            else if (args.Key == Key.LeftShift || args.Key == Key.C)
                userInput.IsPressShift = false;
            else if (args.Key == Key.Space)
                userInput.IsPressShift = false;
            else if (args.Key == Key.Escape)
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
