using System;

namespace TheLastHope.Helpers
{
    /// <summary>
    /// Удобно отсчитывать время
    /// </summary>
    public class Timer
    {
        private DateTime _start;
        public float _elapsed = -1;

        public TimeSpan Duration { get; private set; }
        /// <summary>
        /// Старт отсчета
        /// </summary>
        /// <param name="elapsed">сколько отсчитывать секунд</param>
        public void Start(float elapsed)
        {
            _elapsed = elapsed;
            _start = DateTime.Now;
            Duration = TimeSpan.Zero;
        }

        public void Update()
        {
            if (_elapsed > 0)
            {
                Duration = DateTime.Now - _start;

                if (Duration.TotalSeconds > _elapsed)
                {
                    _elapsed = 0;
                }
            }
            else if (_elapsed == 0)
            {
                _elapsed = -1;
            }
        }
        /// <summary>
        /// Отсчет закончен?
        /// </summary>
        /// <returns></returns>
        public bool IsEvent()
        {
            return _elapsed == 0;
        }

        public int TotalSeconds()
        {
            return (int)(_elapsed - Duration.TotalSeconds);
        }
    }
}