using System;
using System.Collections.Generic;
using MCTS.Math;

namespace MCTS.DST.Objects.Fire {
    public abstract class LightSource
    {
        public Vector2i SourcePosition;
        public int SecondsRemaining;

        public bool CloseEnoughToLight(Vector2i walterPosition)
        {
            return DSTHelper.getRealDistance(walterPosition, SourcePosition) < 2;
        }
        public void PassTime(int timeToPass) {
            this.SecondsRemaining -= timeToPass;
            this.SecondsRemaining = System.Math.Max(0, SecondsRemaining);
        }


        public abstract bool CanAddFuel();
        public abstract void AddFuel(Fuel fuel);
    }

    public class LightSourcesManager
    {
        private readonly List<LightSource> _sources = new List<LightSource>();
        private int _torchTime;
        
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

                    lightSource.PassTime(timeToPass);
                }
            }

            if (torchWasEquipped)
            {
                var torchDarkTime = _torchTime >= timeToPass ? 0 : (timeToPass - _torchTime);
                inDarkness = System.Math.Min(inDarkness, torchDarkTime);

                _torchTime -= timeToPass;
            }

            return inDarkness;
        }

        public void addNewLightSource(DSTObject lightSource)
        {
            //position
            //name
            //timeToBurn
            if (lightSource.GetEntityType().Equals("campfire"))
            {
                //TODO
            } else if (lightSource.GetEntityType().Equals("firepit")) {
                //TODO
            } else if (lightSource.GetEntityType().Equals("torch")) {
                //TODO
            } else
            {
                //dont know what source is that...
                throw new NotImplementedException();
            }
        }
    }
}
