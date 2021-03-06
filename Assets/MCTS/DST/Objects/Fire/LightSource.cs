﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MCTS.Math;
using Utilities;

namespace MCTS.DST.Objects.Fire {
    public abstract class LightSource
    {
        public Vector2i SourcePosition;
        public int SecondsRemaining;
        public DSTObject fireOBj;

        public LightSource(DSTObject obj)
        {
            fireOBj = obj;
        }

        public LightSource(LightSource lightSource)
        {
            var position = lightSource.SourcePosition;
            SourcePosition = new Vector2i(position.x, position.y);
            SecondsRemaining = lightSource.SecondsRemaining;
            fireOBj = lightSource.fireOBj;
        }

        public bool CloseEnoughToLight(Vector2i walterPosition)
        {
            return DSTHelper.getRealDistance(walterPosition, SourcePosition) < 4;
        }
        public void PassTime(int timeToPass) {
            this.SecondsRemaining -= timeToPass;
            this.SecondsRemaining = System.Math.Max(0, SecondsRemaining);
        }


        public abstract bool CanAddFuel();
        public abstract void AddFuel(Fuel fuel);

        public abstract LightSource generateClone();
    }

    public class LightSourcesManager
    {
        public readonly List<LightSource> _sources = new List<LightSource>();
        private int _torchTime = 0;

        public LightSourcesManager(LightSourcesManager lightsManager)
        {
            _sources = lightsManager._sources.Select(slot => slot.generateClone()).ToList();
            _torchTime = lightsManager._torchTime;
        }

        public LightSourcesManager() {
        }

        public int HowManyTimeInDarkness(int timeToPass, bool torchWasEquipped, Vector2i walterStartPosition, Vector2i walterEndPosition)
        {
            int inDarkness = timeToPass;

            foreach (var lightSource in _sources)
            {
                if (lightSource.SecondsRemaining > 0)
                {
                    if (lightSource.CloseEnoughToLight(walterStartPosition) &&
                        lightSource.CloseEnoughToLight(walterEndPosition))
                    {
                        var thisSourceDarkTime =
                            lightSource.SecondsRemaining >= timeToPass
                                ? 0
                                : (timeToPass - lightSource.SecondsRemaining);
                        inDarkness = System.Math.Min(inDarkness, thisSourceDarkTime);
                    }
                    else if (lightSource.CloseEnoughToLight(walterStartPosition))
                    {
                        inDarkness = System.Math.Min(inDarkness, timeToPass - 1);
                    }
                    else if (lightSource.CloseEnoughToLight(walterEndPosition))
                    {
                        inDarkness = System.Math.Min(inDarkness, timeToPass - 1);
                    }

                }
            }

            if (torchWasEquipped)
            {
                var torchDarkTime = _torchTime >= timeToPass ? 0 : (timeToPass - _torchTime);
                inDarkness = System.Math.Min(inDarkness, torchDarkTime);
            }

            return inDarkness;
        }

        public void PassTime(int timeToPass, bool torchEquiped, bool torchInInventory)
        {
            foreach (var lightSource in _sources)
            {
                lightSource.PassTime(timeToPass);
            }

            if (torchEquiped)
            {
                _torchTime -= timeToPass;
            }
            else if(!torchInInventory)
            {
                _torchTime = 0;
            }

        }

        public void addNewLightSource(DSTObject lightSource)
        {
            string entType = lightSource.GetEntityType();
            //position
            //name
            //timeToBurn

            if (entType.Equals("campfire")) {
                _sources.Add(new Campfire(lightSource.GetPosition(),120/* lightSource.TimeToBurn*/,lightSource));
            } else if (entType.Equals("firepit")) {
                _sources.Add(new Firepit(lightSource.GetPosition(),0/* lightSource.TimeToBurn*/, lightSource));
            } else if (entType.Equals("torch")) {
                _torchTime += lightSource.TimeToBurn;
            } else {
                Debug.WriteLine("Unknown light source " + entType);
                //dont know what source is that...
                throw new NotImplementedException();
            }
        } 
    }
}
