using System.IO;

namespace TheLastHope.Management
{
    public class LevelReader
    {
        string levelToRead = string.Empty;
        string[] level;
        char devider;


        public LevelReader(string levelToRead, char devider)
        {
            this.levelToRead = levelToRead;
            this.devider = devider;

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
                return level[lineNumber].Split(devider);
            }
            else
            {
                return new string[15];
            }
        }

    }
}

