using System.IO;

namespace TheLastHope.Management
{
    public class LevelReader
    {
        public string levelToRead = string.Empty;
        string[] level;
        char divider;


        public LevelReader(string levelToRead, char divider)
        {
            this.levelToRead = levelToRead;
            this.divider = divider;

            if (File.Exists(levelToRead))
            {
                level = File.ReadAllLines(levelToRead);
            }
        }
        /// <summary>
        /// Returns array of code of gameObject for each horizontal line.
        /// </summary>
        /// <param name="lineNumber">Number of current vertical line.</param>
        /// <returns></returns>
        public string[] GetLine(int lineNumber)
        {
            if (level != null)
            {
                return level[lineNumber].Split(divider);
            }
            else
            {
                return null;
            }
        }

    }
}

