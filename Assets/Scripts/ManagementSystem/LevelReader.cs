using System.IO;

namespace TheLastHope.Management
{
    /// <summary>
    /// Is used for getting level's map from file.
    /// </summary>
    public class LevelReader
    {
        string levelToRead = string.Empty;
        string[] level;
        char divider;

        /// <summary>
        /// Reads map from file.
        /// </summary>
        /// <param name="levelToRead">Name of map-file without extension.</param>
        /// <param name="devider">Separator of elements in one string of map.</param>
        public LevelReader(string levelToRead, char devider)
        {
            this.levelToRead = levelToRead;
            this.divider = devider;

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
                return new string[15];
            }
        }

    }
}

