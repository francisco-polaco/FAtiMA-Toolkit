using System;
using MCTS.DST.Actions;
using MCTS.DST.WorldModels;
using Action = MCTS.DST.Actions.Action;

namespace MCTS.DST
{
    public class MCTSBiasedAlgorithm : MCTSAlgorithm
    {
        protected override Action GuidedAction(WorldModel currentPlayoutState)
        {
            var possibleActions = currentPlayoutState.GetExecutableActions();

            foreach (var possibleAction in possibleActions)
            {
                var action = possibleAction as WalktoAction;
                if (action != null)
                {
                    if (action.GetEntityType().Equals("campfire"))
                    {
                        return possibleAction;
                    }
                }

                var action2 = possibleAction as BuildAction;
                if (action2 != null)
                {
                    if (action2.GetEntityType().Equals("campfire"))
                    {
                        var random = RandomGenerator.Next(9);
                        if (random > 2)
                        { 
                            return possibleAction;
                        }

                    } 
                }
            } 

            return base.GuidedAction(currentPlayoutState);
        }
    }
}