using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePinBallApplication
{
    
    public enum Direction
    {
        Left,
        Right
    }

    public class Gate
    {
        #region Members

        public static int StaticGateID;
        public static int StaticAmountOfTimeRootGateOpenedLeft;
        private static int[]  staicStatisticeContainerCounterArry;
        public static int[] StaicStatisticeContainerCounterArry
        {
            get
            {
                if (staicStatisticeContainerCounterArry == null)
                {
                    staicStatisticeContainerCounterArry = new int[16];
                }

                return staicStatisticeContainerCounterArry;
            }
        }

        private string gateAlphabeticalID;
        private int gateID;
        private int gateLevel;
        private Gate leftGate;
        private Gate rightGate;
        private Direction openDirection;

        #endregion


        #region Public Functions
        public Gate(bool isRoot = false, bool withStatistics = false)
        {
            if (isRoot)
                Gate.StaticGateID = 0;
                

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            openDirection = ((rand.Next() % 2) == 0) ? Direction.Left : Direction.Right;

            if (withStatistics)
            {
                if (openDirection == Direction.Left)
                    StaticAmountOfTimeRootGateOpenedLeft++;
            }
        }

        public void BuildTree(int level)
        {
            gateLevel = level;
            if (level > 0)
            {
                leftGate = new Gate();
                leftGate.BuildTree(level - 1);
                rightGate = new Gate();
                rightGate.BuildTree(level - 1);
            }
            else
            {
                gateID = StaticGateID;
                gateAlphabeticalID  = GateIndex2AlphabeticalString(++StaticGateID, true);
            }
        }

        public string ReciveBall(bool withStatistics)
        {
            string gatesWithBalls;
            if (gateLevel == 0)
            {
                gatesWithBalls = gateAlphabeticalID + ",";
                if (withStatistics)
                {
                    StaicStatisticeContainerCounterArry[gateID]++;
                }
            }
            else
            {
                if (openDirection == Direction.Left)
                    gatesWithBalls = leftGate.ReciveBall(withStatistics);
                else
                    gatesWithBalls = rightGate.ReciveBall(withStatistics);
            }

            ChangeGateDirection();
            return gatesWithBalls;
        }
        #endregion

        #region Private Functions
        private void ChangeGateDirection()
        {
            if (openDirection == Direction.Left)
                openDirection = Direction.Right;
            else
                openDirection = Direction.Left;
        }

        private static string GateIndex2AlphabeticalString(int number, bool isCaps)
        {
            string aphabeticalNumber = string.Empty;

            int prefixNum = number / 27;

            if (prefixNum > 0)
            {
                aphabeticalNumber = ((Char)(64 + prefixNum)).ToString();
                aphabeticalNumber += ((Char)(65 + (number - (27 * prefixNum)))).ToString();
            }
            else
            {
                aphabeticalNumber += ((Char)(64 + number)).ToString();
            }

            return aphabeticalNumber;
        }

    } 
    #endregion
}
