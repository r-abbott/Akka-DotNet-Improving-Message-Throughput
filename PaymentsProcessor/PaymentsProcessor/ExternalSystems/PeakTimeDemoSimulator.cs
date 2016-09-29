using System;

namespace PaymentsProcessor.ExternalSystems
{
    static class PeakTimeDemoSimulator
    {
        private static DateTime _demoStartTime;
        private static int _stayPeakTimeForSeconds;

        public static bool IsPeakHours
        {
            get
            {
                var elapsedTimeSinceStartingDemo = DateTime.Now.Subtract(_demoStartTime).TotalSeconds;
                return elapsedTimeSinceStartingDemo < _stayPeakTimeForSeconds;
            }
        }

        public static void StartDemo(int stayPeakTimeForSeconds)
        {
            _demoStartTime = DateTime.Now;
            _stayPeakTimeForSeconds = stayPeakTimeForSeconds;
        }
    }
}